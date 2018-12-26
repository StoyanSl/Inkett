using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Specifications;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    public class TattooController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IAsyncRepository<Profile> _profileRepository;

        public TattooController(ITattooViewModelService tattooViewModelService,
           UserManager<ApplicationUser> userManager,
           IAsyncRepository<Profile> profileRepository)
        {
            _tattooViewModelService = tattooViewModelService;
            _userManager = userManager;
            _profileRepository = profileRepository;

        }
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var viewModel = await _tattooViewModelService.GetCreateTattooViewModelAsync(user.Id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTattooViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var spec = new ProfileByAccountIdSpecification(user.Id);
            var profile = await _profileRepository.GetSingleBySpec(spec);
            await _tattooViewModelService.CreateTattooByViewModel(viewModel, profile.Id);
            return RedirectToAction("Index","Profile");
        }

    }
}
