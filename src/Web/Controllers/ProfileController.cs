using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileViewModelService _profileViewModelService;
        public ProfileController(
          UserManager<ApplicationUser> userManager,
          IProfileViewModelService profileViewModelService
          )
        {
            _userManager = userManager;
            _profileViewModelService = profileViewModelService;
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
            var profileViewModel = await _profileViewModelService.GetProfileViewModel(user.Id);
            return View(profileViewModel);
        }
    }
}
