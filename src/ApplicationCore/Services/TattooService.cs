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
        private readonly IImageService _imageService;

        public TattooService(IAsyncRepository<Tattoo> tattooRepository,
            IImageService imageService)
        {
            _tattooRepository = tattooRepository;
            _imageService = imageService;
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

        public Task<Tattoo> GetTattooById(int id)
        {
            return _tattooRepository.GetByIdAsync(id);
        }
        public Task<Tattoo> GetTattooWithStyles(int id)
        {
            var spec = new TattooWithStylesSpecification(id);
            return _tattooRepository.GetSingleBySpec(spec);
        }
    }
}
