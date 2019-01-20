using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inkett.Web.Models;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private const int ItemsPerPage = 9;
        private readonly ITattooService _tattooService;
        private readonly IProfileService _profileService;
        private readonly IHomeViewModelService _homeViewModelService;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileViewModelService _profileViewModelService;

        public HomeController(ITattooService tattooService,
          IProfileService profileService ,
          IHomeViewModelService homeViewModelService,
           ITattooViewModelService tattooViewModelService,
           IProfileViewModelService profileViewModelService)
        {
            _tattooService = tattooService;
            _profileService = profileService;
            _homeViewModelService = homeViewModelService;
            _tattooViewModelService = tattooViewModelService;
            _profileViewModelService = profileViewModelService; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var page = 0;
            var tattoos = await _tattooService.GetTopTattoos(page, ItemsPerPage);
            var profiles = await _profileService.GetTopProfiles(page, ItemsPerPage);
            var viewModel = _homeViewModelService.GetHomeIndexViewModel(profiles, tattoos);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Tattoos()
        {
            var page = 0;
            var tattoos = await _tattooService.GetTattoos(page, ItemsPerPage);
            var viewModels = tattoos.Select(x => _tattooViewModelService.GetTattooListedViewModel(x)).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> TopTattoos()
        {
            var page = 0;
            var tattoos = await _tattooService.GetTopTattoos(page, ItemsPerPage);
            var viewModels = tattoos.Select(x => _tattooViewModelService.GetTattooListedViewModel(x)).ToList();
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
            var viewModel = tattoos.Select(x => _tattooViewModelService.GetTattooListedViewModel(x)).ToList();
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
            var viewModel = tattoos.Select(x => _tattooViewModelService.GetTattooListedViewModel(x)).ToList();
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
