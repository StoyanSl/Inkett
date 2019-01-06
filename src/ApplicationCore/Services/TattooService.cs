using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class TattooService : ITattooService
    {
        private readonly IAsyncRepository<Tattoo> _tattooRepository;
        private readonly IAsyncRepository<Like> _likeRepository;
        private readonly IImageService _imageService;

        public TattooService(IAsyncRepository<Tattoo> tattooRepository,
            IAsyncRepository<Like> likeRepository,
            IImageService imageService)
        {
            _tattooRepository = tattooRepository;
            _likeRepository = likeRepository;
            _imageService = imageService;
        }

        public async Task CreateLike(int profileId, int tattooId)
        {
            var like = new Like() { ProfileId = profileId, TattooId = tattooId };
           await  _likeRepository.AddAsync(like);
        }

        public async Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds, int profileId, int albumId)
        {
            var result = _imageService.UploadImage(tattooPicture);
            var tattoo = new Tattoo
            {
                ProfileId = profileId,
                Description = description??string.Empty,
                AlbumId = albumId != 0 ?  albumId: (int?)null,
                TattooPictureUri = result.ImageUri
            };
            foreach (var id in styleIds)
            {
                tattoo.TattooStyles.Add(new TattooStyle() { Tattoo = tattoo, StyleId = id });
            }
            tattoo = await _tattooRepository.AddAsync(tattoo);
        }
        public async Task RemoveLike(int profileId, int tattooId)
        {
            var spec = new LikeSpecification(profileId, tattooId);
            var like = await _likeRepository.GetSingleBySpec(spec);
            await _likeRepository.DeleteAsync(like);
        }
        public Task<Tattoo> GetTattooById(int id)
        {
            return _tattooRepository.GetByIdAsync(id);
        }
        public Task<Tattoo> GetTattooWithStyles(int id)
        {
            var spec = new TattooWithStylesSpecification(id);
            return _tattooRepository.GetSingleBySpec(spec);
        }

        public async Task<IReadOnlyCollection<Tattoo>> GetTattoosByStyle(int pageIndex, int itemsPerPage ,int id)
        {
            var spec = new TattooByStyleSpecification(pageIndex*itemsPerPage,itemsPerPage,id);
            return await _tattooRepository.ListAsync(spec);
        }
    }
}
