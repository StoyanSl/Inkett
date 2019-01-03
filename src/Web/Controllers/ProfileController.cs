﻿using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Inkett.Web.Controllers
{
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
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            if (id==0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profileViewModel = await _profileViewModelService.GetProfileViewModel(id);
            return View(profileViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Tattoos(int id)
        {
            if (id == 0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profileTattoosViewModel = await _profileViewModelService.GetProfileTattoosViewModel(id);
            if (profileTattoosViewModel.ProfileViewModel.Id == _userManager.GetProfileId(User))
            {
                profileTattoosViewModel.IsOwner = true;
            }
            return View(profileTattoosViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Albums(int id)
        {
            if (id == 0)
            {
                id = _userManager.GetProfileId(User);
            }
            var profileAlbumsViewModel = await _profileViewModelService.GetProfileAlbumsViewModel(id);
            if (profileAlbumsViewModel.ProfileViewModel.Id== _userManager.GetProfileId(User))
            {
                profileAlbumsViewModel.IsOwner = true;
            }
            return View(profileAlbumsViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var profileId = _userManager.GetProfileId(User);
            var profileViewModel = await _profileViewModelService.GetEditProfileViewModel(profileId);
            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProfileViewModel profileBindingModel)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("Edit");
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
