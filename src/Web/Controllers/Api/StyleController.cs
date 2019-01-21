using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers.Api
{
    public class StyleController:BaseApiController
    {

        private readonly IStyleService _styleService;
        private readonly ITattooService _tattooService;
        private readonly ITattooViewModelService _tattooViewModelService;

        public StyleController(IStyleService styleService,
            ITattooService tattooService,
            ITattooViewModelService tattooViewModelService)
        {
            _styleService = styleService;
            _tattooService = tattooService;
            _tattooViewModelService = tattooViewModelService;
        }

        [HttpPost]
        public async Task<PartialViewResult> GetTattoos(int id, int? page)
        {
            int tattoosPerPage = 9;
            var style = await _styleService.GetStyleById(id);
            var tattoos = await _tattooService.GetTattoosByStyle(page ?? 0, tattoosPerPage, id);
            var tattoosViewModels = tattoos.Select(x => _tattooViewModelService.GetTattooListedViewModel(x)).ToList();
            var partial = PartialView("~/Views/Shared/_ListedTattoosContainer.cshtml", tattoosViewModels);
            return partial;
        }
    }
}
