using Inkett.ApplicationCore.Entitites.Profile;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Repositories
{
    public interface IProfileRepository:IRepository<Profile>, IAsyncRepository<Profile>
    {
        Profile GetByAccountId(string id);
        Task<Profile> GetByAccountIdAsync(string id);
    }
}
