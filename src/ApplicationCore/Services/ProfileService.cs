using Inkett.ApplicationCore.Entitites.Profile;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            this._profileRepository = profileRepository;
        }

        public async Task CreateProfileAsync(string accountId, string userName, string profileDescription)
        {
            var profile = new Profile(accountId, userName, profileDescription);
            await _profileRepository.AddAsync(profile);
        }

        public Task<Profile> GetProfileByAccountId(string id)
        {
            return _profileRepository.GetByAccountIdAsync(id);
        }
    }
}
