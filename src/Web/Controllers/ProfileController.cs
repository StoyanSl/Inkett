using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Common;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
            var userProfileId = _userManager.GetProfileId(User);
            var profile = await _profileService.GetProfileWithTattoos(id);
            if (profile == null)
            {
                return NotFound();
            }
            var profileTattoosViewModel = _profileViewModelService
                .GetProfileTattoosViewModel(profile, _userManager.GetProfileId(User));
            return View(profileTattoosViewModel);
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
            var profileTattoosViewModel = _profileViewModelService
                .GetProfileTattoosViewModel(profile, _userManager.GetProfileId(User));

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
            var profileTattoosViewModel = _profileViewModelService
                .GetProfileLikedTattoosViewModel(profile, _userManager.GetProfileId(User));

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
            var profileAlbumsViewModel = _profileViewModelService.GetProfileAlbumsViewModel(profile, _userManager.GetProfileId(User));

            return View(profileAlbumsViewModel);
        }

        [HttpPost]
        [Route("FollowProfile")]
        public async Task<IActionResult> FollowProfile(int profileId)
        {
            var followerId = _userManager.GetProfileId(User);
            if (profileId == followerId)
            {
                return NotFound();
            }
            await _profileService.CreateFollow(followerId, profileId);
            return Ok();
        }

        [HttpPost]
        [Route("UnFollowProfile")]
        public async Task<IActionResult> UnFollowProfile(int profileId)
        {
            var followerId = _userManager.GetProfileId(User);
            if (profileId == followerId)
            {
                return NotFound();
            }
            await _profileService.RemoveFollow(followerId, profileId);
            return Ok();
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
            await _profileService.UpdateProfileDescription(profileId, profileBindingModel.Description);
            return RedirectToAction("Index");
        }
    }
}
