using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Inkett.ApplicationCore.Interfaces.Services;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Entitites;
using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Specifications;
using System.Linq;

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

        public ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile, int userProfileId)
        {
            var viewModel = new ProfileAlbumsViewModel
            {
                Profile = this.GetProfileViewModel(profile)
            };
            foreach (var album in profile.Albums)
            {
                viewModel.ProfileAlbums.Add(_albumViewModelService.GetAlbumViewModel(album));
            }
            if (profile.Id==userProfileId)
            {
                viewModel.Profile.IsOwner = true ;
            }
            if (profile.Followers.Any(x=>x.ProfileId==userProfileId))
            {
                viewModel.Profile.IsFollowed = true;
            }
            return viewModel;
        }

        public ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile, int userProfileId)
        {
            
            var viewModel = new ProfileTattoosViewModel();
            viewModel.Profile = this.GetProfileViewModel(profile);
            foreach (var tattoo in profile.Tattoos)
            {
                viewModel.Tattoos.Add(_tattooViewModelService.GetListedTattooViewModel(tattoo));
            }
            if (profile.Id == userProfileId)
            {
                viewModel.Profile.IsOwner = true;
            }
            if (profile.Followers.Any(x => x.ProfileId == userProfileId))
            {
                viewModel.Profile.IsFollowed = true;
            }
            return viewModel;
        }

        public ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile, int userProfileId)
        {
            var viewModel = new ProfileTattoosViewModel();
            viewModel.Profile = this.GetProfileViewModel(profile);
            foreach (var like in profile.Likes)
            {
                viewModel.Tattoos.Add(_tattooViewModelService.GetListedTattooViewModel(like.Tattoo));
            }
            if (profile.Id == userProfileId)
            {
                viewModel.Profile.IsOwner = true;
            }
            if (profile.Followers.Any(x => x.ProfileId == userProfileId))
            {
                viewModel.Profile.IsFollowed = true;
            }
            return viewModel;
        }
    }
}
