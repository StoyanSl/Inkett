using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inkett.Web.Models;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Inkett.Web.Viewmodels.Home;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const int ItemsPerPage = 9;
        private readonly ITattooService _tattooService;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IProfileService _profileService;

        public HomeController(ITattooService tattooService,
          ITattooViewModelService tattooViewModelService,
          IProfileService profileService,
           IProfileViewModelService profileViewModelService)
        {
            _tattooService = tattooService;
            _tattooViewModelService = tattooViewModelService;
            _profileService = profileService;
            _profileViewModelService = profileViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var page = 0;
            var viewModel = new HomeIndexViewModel();
            var tattoos = await _tattooService.GetTopTattoos(page, ItemsPerPage);
            var profiles = await _profileService.GetTopProfiles(page, ItemsPerPage);
            viewModel.ListedTattooViewModels = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            viewModel.ProfileViewModels = _profileViewModelService.GetProfilesViewModels(profiles);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Tattoos()
        {
            var page = 0;
            var tattoos = await _tattooService.GetTattoos(page, ItemsPerPage);
            var viewModels = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> TopTattoos()
        {
            var page = 0;
            var tattoos = await _tattooService.GetTopTattoos(page, ItemsPerPage);
            var viewModels = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> TopProfiles()
        {
            var page = 0;
            var profiles = await _profileService.GetTopProfiles(page, ItemsPerPage);
            var viewModels = _profileViewModelService.GetProfilesViewModels(profiles);
            return View(viewModels);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetTopTattoos(int? page)
        {
            var tattoos = await _tattooService.GetTopTattoos(page ?? 0, ItemsPerPage);
            var viewModel = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetTopProfiles(int? page)
        {
            var profiles = await _profileService.GetTopProfiles(page ?? 0, ItemsPerPage);
            var viewModels = _profileViewModelService.GetProfilesViewModels(profiles);
            return PartialView("~/Views/Home/_ListedProfilesContainer.cshtml", viewModels);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetTattoos(int? page)
        {
            var tattoos = await _tattooService.GetTattoos(page ?? 0, ItemsPerPage);
            var viewModel = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", viewModel);
        }

        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
