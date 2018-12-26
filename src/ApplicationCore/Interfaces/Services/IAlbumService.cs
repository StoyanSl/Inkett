using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services    
{
    public interface IAlbumService
    {
        Task CreateAlbum(int profileId);
        Task CreateAlbum(int profileId, string title, string description, IFormFile picture);
        Task EditAlbum(int profileId, int albumId, string title, string description, IFormFile picture);
        Task<bool> AlbumNameExists(int profileId,string albumTitle);
    }
}
