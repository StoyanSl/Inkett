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

        public async Task<bool> AlbumNameExists(int profileId, string albumTitle)
        {
            var spec = new AlbumByProfileSpecification(profileId);
            var albums = await _albumRepository.ListAsync(spec);
            return albums.Any(a => a.Title == albumTitle);
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
        public async Task EditAlbum(int profileId,int albumId, string title, string description, IFormFile picture)
        {
            var spec = new AlbumByProfileSpecification(albumId, profileId);
            var album = await _albumRepository.GetSingleBySpec(spec);
            if (album==null)
            {
                throw new ApplicationException();
            }
            if (picture != null)
            {
                var result = _imageService.UploadImage(picture);
                if (!result.Success)
                {
                    throw new ApplicationException();
                }
                album.AlbumPictureUri = result.ImageUri;
            }
            album.Title = title;
            album.Description = description;
            await _albumRepository.UpdateAsync(album);
        }
        
    }
}
