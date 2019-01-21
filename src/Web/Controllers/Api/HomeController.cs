using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using System.Linq;

namespace Inkett.Web.Controllers.Api
{
    public class HomeController : BaseApiController
    {
        private const int ItemsPerPage = 9;
        private readonly ITattooService _tattooService;
        private readonly IProfileService _profileService;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileViewModelService _profileViewModelService;

        public HomeController(ITattooService tattooService,
          IProfileService profileService,
           ITattooViewModelService tattooViewModelService,
           IProfileViewModelService profileViewModelService)
        {
            _tattooService = tattooService;
            _profileService = profileService;
            _tattooViewModelService = tattooViewModelService;
            _profileViewModelService = profileViewModelService;
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
    }
}
