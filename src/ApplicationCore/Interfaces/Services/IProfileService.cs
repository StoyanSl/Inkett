using Inkett.ApplicationCore.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IProfileService
    {
        Task<Profile> CreateProfileAsync(string accountId, string userName, string profileDescription);
        Task CreateFollow(int followerId, int followedId);
        Task RemoveFollow(int followerId, int followedId);
        bool ProfileNameExists(string profileName);
        Task UpdateProfilePicture(int profileId, string pictureUrl);
        Task UpdateCoverPicture(int profileId, string pictureUrl);
        Task UpdateProfileDescription(int profileId, string description);
        Task<IReadOnlyCollection<Profile>> GetTopProfiles(int page, int itemsPerPage);
        Task<Profile> GetProfileWithAlbums(int profileId);
        Task<Profile> GetProfileWithLikes(int profileId);
        Task<Profile> GetProfileWithTattoos(int profileId);
        Task<Profile> GetProfileById(int profileId);
    }
}
