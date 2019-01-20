using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IAsyncRepository<Profile> _profileRepository;
        private readonly IAsyncRepository<Follow> _followRepository;
        private readonly IAlbumService _albumService;
        public ProfileService(IAsyncRepository<Profile> profileRepository,
            IAsyncRepository<Follow> followRepository,
            IAlbumService albumService)
        {
            _profileRepository = profileRepository;
            _albumService = albumService;
            _followRepository = followRepository;
        }

        public async Task<Profile> CreateProfileAsync(string accountId, string userName, string profileDescription)
        {
            var profile = new Profile(accountId, userName, profileDescription);
            profile = await _profileRepository.AddAsync(profile);
            await _albumService.CreateAlbum(profile.Id);
            return profile;
        }

        public async Task CreateFollow(int followerId, int followedId)
        {
            var follow = new Follow(followerId, followedId);
           await  _followRepository.AddAsync(follow);
        }

        public async Task RemoveFollow(int followerId, int followedId)
        {
            var spec = new FollowSpecification(followerId,followedId);
            var follow = await _followRepository.GetSingleBySpec(spec);
            await _followRepository.DeleteAsync(follow);
        }

        public async Task UpdateProfilePicture(int profileId, string pictureUrl)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            profile.ProfilePictureUri = pictureUrl;
            await _profileRepository.UpdateAsync(profile);
        }

        public async Task UpdateCoverPicture(int profileId, string pictureUrl)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            profile.CoverPictureUri = pictureUrl;
            await _profileRepository.UpdateAsync(profile);
        }

        public bool ProfileNameExists(string profileName)
        {
            return _profileRepository.ListAllAsync().Result.Any(x => x.Name == profileName);
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

        public async Task<IReadOnlyCollection<Profile>> GetTopProfiles(int pageIndex, int itemsPerPage)
        {
            var spec = new ProfileByFollowersSpecification(pageIndex * itemsPerPage, itemsPerPage);
            return await _profileRepository.ListAsync(spec);
        }

        public async Task UpdateProfileDescription(int profileId, string description)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            profile.Description = description;
            await _profileRepository.UpdateAsync(profile);
        }
    }
}
