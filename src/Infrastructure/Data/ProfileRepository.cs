using Inkett.ApplicationCore.Entitites.Profile;
using Inkett.ApplicationCore.Interfaces.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inkett.Infrastructure.Data
{
    public class ProfileRepository : EfRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(InkettContext dbContext) : base(dbContext)
        {
        }

        public Profile GetByAccountId(string id)
        {
            return _dbContext.Profiles.FirstOrDefault(x=>x.AccountId==id);
        }

        public Task<Profile> GetByAccountIdAsync(string id)
        {
            return _dbContext.Profiles.FirstOrDefaultAsync(x => x.AccountId == id);
        }
    }
}
