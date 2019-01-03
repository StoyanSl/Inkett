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
        private readonly IAlbumViewModelService _albumViewModelService;
        private readonly ITattooViewModelService _tattooViewModelService;

        public ProfileViewModelService(IAsyncRepository<Profile> profileRepository,
            IAlbumViewModelService albumViewModelService,
            ITattooViewModelService tattooViewModelService)
        {
            _tattooViewModelService = tattooViewModelService;
            _profileRepository = profileRepository;
            _albumViewModelService = albumViewModelService;
        }

        public async Task<ProfileViewModel> GetProfileViewModel(int profileId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);

            var profileViewModel = new ProfileViewModel
            {
                Id = profile.Id,
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
        public async Task<ProfileAlbumsViewModel> GetProfileAlbumsViewModel(int profileId)
        {
            var spec = new ProfileWithAlbumsSpecification(profileId);
            var profile = await _profileRepository.GetSingleBySpec(spec);
            var viewModel = new ProfileAlbumsViewModel();
            viewModel.ProfileViewModel = this.GetProfileViewModel(profile);
            foreach (var album in profile.Albums)
            {
                viewModel.ProfileAlbumViewModels.Add(_albumViewModelService.GetAlbumViewModel(album));
            }
            return viewModel;
        }
        public async Task<ProfileTattoosViewModel> GetProfileTattoosViewModel(int profileId)
        {
            var spec = new ProfileWithTattoosSpecification(profileId);
            var profile = await _profileRepository.GetSingleBySpec(spec);
            var viewModel = new ProfileTattoosViewModel();
            viewModel.ProfileViewModel = this.GetProfileViewModel(profile);
            foreach (var tattoo in profile.Tattoos)
            {
                viewModel.ProfileTattoos.Add(_tattooViewModelService.GetListedTattooViewModel(tattoo));
            }
            return viewModel;
        }
        public ProfileViewModel GetProfileViewModel(Profile profile)
        {
            return new ProfileViewModel
            {
                Id=profile.Id,
                ProfileName = profile.ProfileName,
                ProfileDescription = profile.ProfileDescription,
                ProfilePictureUri = profile.ProfilePicture,
                CoverPictureUri = profile.CoverPicture
            };
        }
    }
}
