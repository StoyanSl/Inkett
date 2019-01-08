using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inkett.Web.Models;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITattooService _tattooService;
        private readonly ITattooViewModelService _tattooViewModelService;
        public HomeController(ITattooService tattooService,
          ITattooViewModelService tattooViewModelService)
        {
            _tattooService = tattooService;
            _tattooViewModelService = tattooViewModelService;
        }
        public async Task<IActionResult> Index()
        {
            var page = 0;
            var itemsPerPage = 9;
            var tattoos = await _tattooService.GetTopTattoos(page, itemsPerPage);
            var viewModel = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetTattoos(int? page)
        {
            var itemsPerPage = 9;
            var tattoos = await _tattooService.GetTopTattoos(page??0,itemsPerPage);
            var viewModel = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", viewModel);
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
