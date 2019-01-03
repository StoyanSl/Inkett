using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    public class TattooController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITattooService _tattooService;

        public TattooController(ITattooViewModelService tattooViewModelService,
           InkettUserManager userManager,
           IAuthorizationService authorizationService,
            ITattooService tattooService)
        {
            _tattooViewModelService = tattooViewModelService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _tattooService = tattooService;
    }
        public async Task<IActionResult> Create()
        {
            var profileId =  _userManager.GetProfileId(User);
            var viewModel = await _tattooViewModelService.GetCreateTattooViewModel(profileId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TattooViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.CreateTattooByViewModel(viewModel, profileId);
            }
           
            return RedirectToAction("Index","Profile");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var profileId = _userManager.GetProfileId(User);
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            var authorizeResultTask =  _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            var viewModelTask = _tattooViewModelService.GetEditTattooViewModel(tattoo);
            if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
            {
                return Unauthorized();
            }
            return View(viewModelTask.GetAwaiter().GetResult());
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            var authorizeResult = await _authorizationService.AuthorizeAsync(User,tattoo, "EditPolicy");
            if (!authorizeResult.Succeeded)
            {
                return Unauthorized();
            }
            return RedirectToAction("Index","Home");
        }

    }
}
