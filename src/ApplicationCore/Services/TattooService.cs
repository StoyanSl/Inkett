using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class TattooService:ITattooService
    {
        private readonly IAsyncRepository<Tattoo> _tattooRepository;
        private readonly IImageService _imageService;

        public TattooService(IAsyncRepository<Tattoo> tattooRepository,
            IImageService imageService)
        {
            _tattooRepository = tattooRepository;
            _imageService = imageService;
        }

        public async Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds, int profileId,int albumId)
        {
            var result = _imageService.UploadImage(tattooPicture);
            var tattoo = new Tattoo
            {
                ProfileId = profileId,
                Description = description,
                AlbumId = albumId,
                TattooPictureUri=result.ImageUri
            };
            tattoo = await _tattooRepository.AddAsync(tattoo);
            foreach (var id in styleIds)
            {
                tattoo.TattooStyles.Add(new TattooStyle() { TattooId = tattoo.Id, StyleId = id });
            }
           await _tattooRepository.UpdateAsync(tattoo);
        }
    }
}
