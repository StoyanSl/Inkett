using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Common;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Inkett.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IImageService _imageService;
        private readonly IProfileService _profileService;
        private readonly IAlbumService _albumService;
        public ProfileController(
          InkettUserManager userManager,
          IProfileViewModelService profileViewModelService,
          IImageService imageService,
          IProfileService profileService,
          IAlbumService albumService
          )
        {
            _userManager = userManager;
            _profileViewModelService = profileViewModelService;
            _profileService = profileService;
            _imageService = imageService;
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profile = await _profileService.GetProfileById(id);
            if (profile == null)
            {
                return NotFound();
            }
            var profileViewModel = _profileViewModelService.GetProfileViewModel(profile);
            profileViewModel.IsOwner = profileViewModel.Id == _userManager.GetProfileId(User);
            return View(profileViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Tattoos(int id)
        {
            if (id == 0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profile = await _profileService.GetProfileWithTattoos(id);
            if (profile == null)
            {
                return NotFound();
            }
            var profileTattoosViewModel = _profileViewModelService.GetProfileTattoosViewModel(profile);
            if (profileTattoosViewModel.Profile.Id == _userManager.GetProfileId(User))
            {
                profileTattoosViewModel.Profile.IsOwner = true;
            }
            return View(profileTattoosViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LikedTattoos()
        {
            var profileId = _userManager.GetProfileId(User);
            var profile = await _profileService.GetProfileWithLikes(profileId);
            if (profile == null)
            {
                return NotFound();
            }
            var profileTattoosViewModel = _profileViewModelService.GetProfileLikedTattoosViewModel(profile);
            return View(profileTattoosViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Albums(int id)
        {
            if (id == 0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profile = await _profileService.GetProfileWithAlbums(id);
            if (profile == null)
            {
                return NotFound();
            }
            var profileAlbumsViewModel = _profileViewModelService.GetProfileAlbumsViewModel(profile);
            if (profileAlbumsViewModel.Profile.Id == _userManager.GetProfileId(User))
            {
                profileAlbumsViewModel.Profile.IsOwner = true;
            }
            return View(profileAlbumsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var profileId = _userManager.GetProfileId(User);
            var profile = await _profileService.GetProfileWithTattoos(profileId);
            if (profile == null)
            {
                return NotFound();
            }
            var profileViewModel = _profileViewModelService.GetEditProfileViewModel(profile);
            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel profileBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit");
            }
            var profileId = _userManager.GetProfileId(User);
            if (profileBindingModel.CoverPictureFile != null)
            {
                var result = _imageService.UploadImage(profileBindingModel.CoverPictureFile, 450, 960);
                if (result.Success)
                {
                    await _profileService.UpdateCoverPicture(profileId, result.ImageUri);
                }
            }
            if (profileBindingModel.ProfilePictureFile != null)
            {
                var result = _imageService.UploadImage(profileBindingModel.ProfilePictureFile, 186, 186);
                if (result.Success)
                {
                    await _profileService.UpdateProfilePicture(profileId, result.ImageUri);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
