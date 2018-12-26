﻿using Inkett.ApplicationCore.Entitites;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IProfileService
    {
        Task CreateProfileAsync(string accountId, string userName, string profileDescription);
        Task<Profile> GetProfileByAccountId(string id);
        bool ProfileNameExists(string profileName);
        Task UpdateProfilePicture(string accountId, string pictureUrl);
        Task UpdateCoverPicture(string accountId, string pictureUrl);
    }
}
