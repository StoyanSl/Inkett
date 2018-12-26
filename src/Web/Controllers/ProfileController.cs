using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Inkett.Web.Viewmodels.Profile.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Inkett.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IImageService _imageService;
        private readonly IProfileService _profileService;
        private readonly IAlbumService _albumService;
        public ProfileController(
          UserManager<ApplicationUser> userManager,
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
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var profileViewModel = await _profileViewModelService.GetProfileViewModel(user.Id);
            return View(profileViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var profileViewModel = await _profileViewModelService.GetEditProfileViewModel(user.Id);
            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditProfileBindingModel profileBindingModel)
        {
            
            if (!ModelState.IsValid)
            {
                RedirectToAction("Edit");
            }
            var user = await _userManager.GetUserAsync(User);
            if (profileBindingModel.CoverPictureFile!=null)
            {
                var result=_imageService.UploadImage(profileBindingModel.CoverPictureFile);
                if (result.Success)
                {
                   await _profileService.UpdateCoverPicture(user.Id,result.ImageUri);
                }
            }
            if (profileBindingModel.ProfilePictureFile != null)
            {
                var result = _imageService.UploadImage(profileBindingModel.ProfilePictureFile);
                if (result.Success)
                {
                    await _profileService.UpdateProfilePicture(user.Id, result.ImageUri);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Albums()
        {
            var user = await _userManager.GetUserAsync(User);
            var profileViewModel = await _profileViewModelService.GetEditProfileViewModel(user.Id);
            return View(profileViewModel);
        }
    }
}
