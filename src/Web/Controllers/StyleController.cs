using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Common;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class StyleController : Controller
    {
        private readonly IStyleService _styleService;
        private readonly IStyleViewModelService _styleViewModelService;
        private readonly ITattooService _tattooService;
        private readonly ITattooViewModelService _tattooViewModelService;

        public StyleController(IStyleService styleService,
            IStyleViewModelService styleViewModelService,
            ITattooService tattooService,
            ITattooViewModelService tattooViewModelService)
        {
            _styleService = styleService;
            _styleViewModelService = styleViewModelService;
            _tattooService = tattooService;
            _tattooViewModelService = tattooViewModelService;
        }
        public async Task<IActionResult> Listing()
        {
            var styles = await _styleService.GetStyles();
            var stylesViewModels = _styleViewModelService.GetStylesViewModels(styles);
            return View(stylesViewModels);
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id, int? page)
        {
            int tattoosPerPage = 9;
            var style = await _styleService.GetStyleById(id);
            if (style==null)
            {
                return NotFound();
            }
            var tattoos = await _tattooService.GetTattoosByStyle(page??0,tattoosPerPage,id);
            var viewModel = _styleViewModelService.GetIndexStyleViewModel(style);
            viewModel.Tattoos =  _tattooViewModelService.GetListedTattooViewModel(tattoos);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetTattoos(int id, int? page)
        {
            int tattoosPerPage = 9;
            var style =await _styleService.GetStyleById(id);
            var tattoos = await  _tattooService.GetTattoosByStyle(page ?? 0, tattoosPerPage, id);
            var viewModel = _styleViewModelService.GetIndexStyleViewModel(style);
            viewModel.Tattoos = _tattooViewModelService.GetListedTattooViewModel(tattoos);
            var partial= PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", viewModel.Tattoos);
            return partial;
        }
        
    }
}
