using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Inkett.ApplicationCore.Interfaces.Services;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Entitites.Profile;
using Ardalis.GuardClauses;

namespace Inkett.Web.Services
{
    public class ProfileViewModelService : IProfileViewModelService
    {
        private readonly IProfileService _profileService;

        public ProfileViewModelService(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<ProfileViewModel> GetProfileViewModel(string accountId)
        {
            var profile = await _profileService.GetProfileByAccountId(accountId);
            Guard.Against.Null(profile,nameof(profile));
            return CreateProfileViewModel(profile);
        }

        private ProfileViewModel CreateProfileViewModel(Profile profile)
        {
            var profileViewModel = new ProfileViewModel() {
            ProfileName=profile.ProfileName,
            ProfileDescription=profile.ProfileDescription,
            ProfilePictureUri=profile.ProfilePicture,
            CoverPictureUri=profile.CoverPicture};
            return profileViewModel;
        }
    }
}
