using Inkett.Web.Interfaces.Services;
using Inkett.ApplicationCore.Interfaces.Repositories;
using System.Collections.Generic;
using Inkett.Web.Models.ViewModels.Profiles;
using AutoMapper;
using System.Linq;

using Profile = Inkett.ApplicationCore.Entitites.Profile;
using Inkett.ApplicationCore.Entitites;

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

        public ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile, int userProfileId)
        {
            var viewModel = Mapper.Map<Profile, ProfileAlbumsViewModel>(profile);
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

        public ProfileAlbumViewModel GetProfileAlbumViewModel(Album album, int userProfileId)
        {
            var viewModel = Mapper.Map<Album, ProfileAlbumViewModel>(album);
            if (viewModel.Profile.Id==userProfileId)
            {
                viewModel.Profile.IsOwner = true;
            }
            return viewModel;
        }

        public ProfileIndexViewModel GetProfileIndexViewModel(Profile profile, int userProfileId)
        {
            var viewModel = Mapper.Map<Profile, ProfileIndexViewModel>(profile);
            if (profile.Id==userProfileId)
            {
                viewModel.Profile.IsOwner = true;
            }
            if (profile.Followers.Any(x => x.ProfileId == userProfileId))
            {
                viewModel.Profile.IsFollowed = true;
            }
            return viewModel;
        }

        public ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile)
        {
            var viewModel = Mapper.Map<Profile, ProfileTattoosViewModel>(profile);
            viewModel.Profile.IsOwner = true;
            return viewModel;
        }

        public List<ProfileViewModel> GetProfilesViewModels(IReadOnlyCollection<Profile> profiles)
        {
            return profiles.Select(x => Mapper.Map<Profile, ProfileViewModel>(x)).ToList();
        }

        public ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile, int userProfileId)
        {
            var viewModel = Mapper.Map<Profile, ProfileTattoosViewModel>(profile);
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

        public ProfileViewModel GetProfileViewModel(Profile profile)
        {
            var viewModel = Mapper.Map<Profile, ProfileViewModel>(profile);
            viewModel.IsOwner = true;
            return viewModel;
        }
    }
}
