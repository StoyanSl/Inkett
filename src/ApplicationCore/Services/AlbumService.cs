using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;

namespace Inkett.ApplicationCore.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAsyncRepository<Album> _albumRepository;
        private readonly IImageService _imageService;

        public AlbumService(IAsyncRepository<Album> albumRepository,
            IImageService imageService)
        {
            _albumRepository = albumRepository;
            _imageService = imageService;
        }

      
        public async Task CreateAlbum(int profileId)
        {
            var album = new Album(profileId);
            await _albumRepository.AddAsync(album);
        }
        public async Task CreateAlbum(int profileId,string title,string description,IFormFile picture)
        {
            if (picture is null)
            {
                var album = new Album(profileId, title, description);
                await _albumRepository.AddAsync(album);
                return;
            }
            var result = _imageService.UploadImage(picture);
            if (result.Success)
            {
                var album = new Album(profileId, title, description,result.ImageUri);
                await _albumRepository.AddAsync(album);
                return;
            }
           
            
        }
        public async Task<Album> EditAlbum(Album album, string title, IFormFile picture)
        {
            if (picture != null)
            {
                var result = _imageService.UploadImage(picture);
                if (!result.Success)
                {
                    return null;
                }
                album.PictureUri = result.ImageUri;
            }
            album.Title = title;
            album = await _albumRepository.UpdateAsync(album);
            return album;
        }

        public Task<Album> GetAlbumById(int id)
        {
            return _albumRepository.GetByIdAsync(id);
        }

        public Task<Album> GetAlbumWithTattoos(int id)
        {
            var album = _albumRepository.GetSingleBySpec(new AlbumWithTattoosSpecification(id));
            return album;
        }

        public Task RemoveAlbum(Album album)
        {
            return  _albumRepository.DeleteAsync(album);
        }
    }
}
