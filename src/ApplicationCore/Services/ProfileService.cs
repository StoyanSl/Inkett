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

        public async Task UpdateProfilePicture(int profileId, string pictureUrl)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            profile.ProfilePicture = pictureUrl;
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task UpdateCoverPicture(int profileId, string pictureUrl)
        {

            var profile = await _profileRepository.GetByIdAsync(profileId);
            profile.CoverPicture = pictureUrl;
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task<Profile> CreateProfileAsync(string accountId, string userName, string profileDescription)
        {
            var profile = new Profile(accountId, userName, profileDescription);
            profile = await _profileRepository.AddAsync(profile);
            await _albumService.CreateAlbum(profile.Id);
            return profile;
        }
        
        public bool ProfileNameExists(string profileName)
        {
            return _profileRepository.ListAllAsync().Result.Any(x => x.ProfileName == profileName);
        }

        public  Task<Profile> GetProfileWithAlbums(int profileId)
        {
            var spec = new ProfileWithAlbumsSpecification(profileId);
            return _profileRepository.GetSingleBySpec(spec);
        }

        public Task<Profile> GetProfileWithLikes(int profileId)
        {
            var spec = new ProfileWithLikesSpecification(profileId);
            return _profileRepository.GetSingleBySpec(spec);
        }


        public  Task<Profile> GetProfileById(int profileId)
        {
           return _profileRepository.GetByIdAsync(profileId);
        }

        public Task<Profile> GetProfileWithTattoos(int profileId)
        {
            var spec = new ProfileWithTattoosSpecification(profileId);
            return _profileRepository.GetSingleBySpec(spec);
        }
    }
}
