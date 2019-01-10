using Inkett.ApplicationCore.Entitites;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface ITattooService
    {
        Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds,int profileId,int albumId);
        Task EditTattoo(Tattoo tattoo, string description, IEnumerable<int> styleIds, int albumId);
        Task RemoveTattoo(Tattoo tattoo);
        Task<Tattoo> GetTattooById(int id);
        Task<Tattoo> GetTattooWithStyles(int id);
        Task<IReadOnlyCollection<Tattoo>> GetTattoos(int page, int itemsPerPage);
        Task<IReadOnlyCollection<Tattoo>> GetTopTattoos(int page,int itemsPerPage);
        Task<IReadOnlyCollection<Tattoo>> GetTattoosByStyle(int pageIndex,int itemsPerPage,int id);
        Task CreateLike(int profileId, int tattooId);
        Task RemoveLike(int profileId, int tattooId);
    }
}
