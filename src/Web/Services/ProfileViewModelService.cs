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

        public  ProfileViewModel GetProfileViewModel(Profile profile)
        {
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

        public  EditProfileViewModel GetEditProfileViewModel(Profile profile)
        {
            var profileViewModel = this.GetProfileViewModel(profile);

            var editProfileViewModel = new EditProfileViewModel()
            {
                Profile = profileViewModel
            };

            return editProfileViewModel;
        }

        public ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile)
        {
            var viewModel = new ProfileAlbumsViewModel();
            viewModel.Profile = this.GetProfileViewModel(profile);
            foreach (var album in profile.Albums)
            {
                viewModel.ProfileAlbums.Add(_albumViewModelService.GetAlbumViewModel(album));
            }
            return viewModel;
        }

        public ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile)
        {
            
            var viewModel = new ProfileTattoosViewModel();
            viewModel.Profile = this.GetProfileViewModel(profile);
            foreach (var tattoo in profile.Tattoos)
            {
                viewModel.Tattoos.Add(_tattooViewModelService.GetListedTattooViewModel(tattoo));
            }
            return viewModel;
        }

        public ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile)
        {
            var viewModel = new ProfileTattoosViewModel();
            viewModel.Profile = this.GetProfileViewModel(profile);
            foreach (var like in profile.Likes)
            {
                viewModel.Tattoos.Add(_tattooViewModelService.GetListedTattooViewModel(like.Tattoo));
            }
            return viewModel;
        }
    }
}
