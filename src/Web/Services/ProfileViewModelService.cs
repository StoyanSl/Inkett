using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Inkett.ApplicationCore.Interfaces.Services;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Entitites;
using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Specifications;

namespace Inkett.Web.Services
{
    public class ProfileViewModelService : IProfileViewModelService
    {
        private readonly IAsyncRepository<Profile> _profileRepository;

        public ProfileViewModelService(IAsyncRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;

        }

        public async Task<ProfileViewModel> GetProfileViewModel(int profileId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);

            var profileViewModel = new ProfileViewModel
            {
                ProfileName = profile.ProfileName,
                ProfileDescription = profile.ProfileDescription,
                ProfilePictureUri = profile.ProfilePicture,
                CoverPictureUri = profile.CoverPicture
            };

            return profileViewModel;
        }
        public async Task<EditProfileViewModel> GetEditProfileViewModel(int profileId)
        {
            var profileViewModel = await this.GetProfileViewModel(profileId);

            var editProfileViewModel = new EditProfileViewModel()
            {
                ProfileViewModel = profileViewModel
            };

            return editProfileViewModel;
        }
        public async Task<ProfileAlbumsViewModel> GetProfileAlbumsViewModel(string accountId)
        {
            var viewModel = new ProfileAlbumsViewModel();
            viewModel.ProfileViewModel = await GetProfileViewModel(1);
            return viewModel;

        }
    }
}
