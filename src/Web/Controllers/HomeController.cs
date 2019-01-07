using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inkett.Web.Models;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;

namespace Inkett.Web.Controllers
{
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
        public async Task<PartialViewResult> GetTattoos( int? page)
        {
            var itemsPerPage = 9;
            var tattoos = await _tattooService.GetTopTattoos(page??0,itemsPerPage);
            var viewModel = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            var partial = PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", viewModel);
            return partial;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
