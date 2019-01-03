using Inkett.ApplicationCore.Entitites;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services    
{
    public interface IAlbumService
    {
        Task CreateAlbum(int profileId);
        Task CreateAlbum(int profileId, string title, string description, IFormFile picture);
        Task<Album> EditAlbum(Album album, string title, IFormFile picture);
        Task<Album> GetAlbumById(int id);
        Task<Album> GetAlbumWithTattoos(int id);
        Task RemoveAlbum(Album album);
    }
}
