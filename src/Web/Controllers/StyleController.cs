using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Common;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        [HttpGet]
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
            var styleName = _styleService.GetStyles().GetAwaiter().GetResult().FirstOrDefault(x => x.Id == id).Name;
            if (styleName==null)
            {
                return NotFound();
            }
            var tattoos = await _tattooService.GetTattoosByStyle(page ?? 0, tattoosPerPage, id);
            if (tattoos == null)
            {
                return NotFound();
            }
            var viewModel = _styleViewModelService.GetIndexStyleViewModel(tattoos,id,styleName);
            return View(viewModel);
        }

        

    }
}
