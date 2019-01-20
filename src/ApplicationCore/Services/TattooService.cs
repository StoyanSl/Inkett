using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class TattooService : ITattooService
    {
        private readonly IAsyncRepository<Tattoo> _tattooRepository;
        private readonly IAsyncRepository<Like> _likeRepository;
        private readonly IAsyncRepository<TattooStyle> _tattooStyleRepository;
        private readonly IImageService _imageService;
        private readonly INotificationService _notificationService;

        public TattooService(IAsyncRepository<Tattoo> tattooRepository,
            IAsyncRepository<Like> likeRepository,
            IAsyncRepository<TattooStyle> tattooStyleRepository,
            IImageService imageService,
            INotificationService notificationService)
        {
            _tattooStyleRepository = tattooStyleRepository;
            _tattooRepository = tattooRepository;
            _likeRepository = likeRepository;
            _imageService = imageService;
            _notificationService = notificationService;
        }

        public async Task CreateLike(int profileId, int tattooId)
        {
            var like = new Like(profileId,tattooId);
            await _likeRepository.AddAsync(like);
        }

        public async Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds, int profileId, int albumId)
        {
            var result = _imageService.UploadImage(tattooPicture);
            var tattoo = new Tattoo
            {
                ProfileId = profileId,
                Description = description,
                AlbumId = albumId != 0 ? albumId : (int?)null,
                PictureUri = result.ImageUri
            };
            foreach (var id in styleIds)
            {
                tattoo.TattooStyles.Add(new TattooStyle() { Tattoo = tattoo, StyleId = id });
            }
            tattoo = await _tattooRepository.AddAsync(tattoo);
            await _notificationService.CreateNotifications(profileId, tattoo.PictureUri, tattoo.Id);
        }

        public async Task EditTattoo(Tattoo tattoo, string description, IEnumerable<int> styleIds, int albumId)
        {
            var stylesToRemove = tattoo.TattooStyles.Where(x => !styleIds.Contains(x.StyleId)).ToList();
            if (stylesToRemove.Count!=0)
            {
                await _tattooStyleRepository.DeleteRange(stylesToRemove);
                foreach (var styleToRemove in stylesToRemove)
                {
                    tattoo.TattooStyles.Remove(styleToRemove);
                }
                
            }

            tattoo.Description = description;
            foreach (var id in styleIds)
            {
                if (!tattoo.TattooStyles.Any(ts=>ts.StyleId==id))
                {
                    tattoo.TattooStyles.Add(new TattooStyle() { Tattoo = tattoo, StyleId = id });
                }
            }
            tattoo.AlbumId = albumId != 0 ? albumId : (int?)null;
            await _tattooRepository.UpdateAsync(tattoo);
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

        public async Task<IReadOnlyCollection<Tattoo>> GetTopTattoos(int pageIndex, int itemsPerPage)
        {
            var spec = new TattooByLikesCountSpecification(pageIndex * itemsPerPage, itemsPerPage);
            return await _tattooRepository.ListAsync(spec);
        }

        public async Task<IReadOnlyCollection<Tattoo>> GetTattoos(int pageIndex, int itemsPerPage)
        {
            var spec = new TattooByOrderSpecification(pageIndex * itemsPerPage, itemsPerPage);
            return await _tattooRepository.ListAsync(spec);

        }

        public async Task<IReadOnlyCollection<Tattoo>> GetTattoosByStyle(int pageIndex, int itemsPerPage, int id)
        {
            var spec = new TattooByStyleSpecification(pageIndex * itemsPerPage, itemsPerPage, id);
            return await _tattooRepository.ListAsync(spec);
        }

        public async Task RemoveTattoo(Tattoo tattoo)
        {
           await _tattooRepository.DeleteAsync(tattoo);
        }
    }
}
