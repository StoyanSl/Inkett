using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IAsyncRepository<Profile> _profileRepository;
        private readonly IAlbumService _albumService;
        public ProfileService(IAsyncRepository<Profile> profileRepository,
            IAlbumService albumService)
        {
            _profileRepository = profileRepository;
            _albumService = albumService;
        }

        public async Task UpdateProfilePicture(string accountId, string pictureUrl)
        {
            var spec = new ProfileByAccountIdSpecification(accountId);
            var profile =  await _profileRepository.GetSingleBySpec(spec);
            profile.ProfilePicture = pictureUrl;
           await _profileRepository.UpdateAsync(profile);
        }
        public async Task UpdateCoverPicture(string accountId, string pictureUrl)
        {
            var spec = new ProfileByAccountIdSpecification(accountId);
            var profile = await _profileRepository.GetSingleBySpec(spec);
            profile.CoverPicture = pictureUrl;
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task CreateProfileAsync(string accountId, string userName, string profileDescription)
        {
            var profile = new Profile(accountId, userName, profileDescription);
            await _profileRepository.AddAsync(profile);
            await _albumService.CreateAlbum(profile.Id);
        }

        public async Task<Profile> GetProfileByAccountId(string id)
        {
            var spec = new ProfileByAccountIdSpecification(id);
            return await _profileRepository.GetSingleBySpec(spec);
        }

        public bool ProfileNameExists(string profileName)
        {
            return _profileRepository.ListAllAsync().Result.Any(x=>x.ProfileName==profileName);
        }

       
    }
}
