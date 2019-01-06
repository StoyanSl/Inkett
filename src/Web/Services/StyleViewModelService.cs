using System.Collections.Generic;
using System.Linq;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Style;
using Inkett.Web.Viewmodels.Tattoo;

namespace Inkett.Web.Services
{
    public class StyleViewModelService : IStyleViewModelService
    {
        public StyleViewModelService()
        {

        }

        public IndexStyleViewModel GetIndexStyleViewModel(Style style)
        {
            var viewModel = new IndexStyleViewModel
            {
                Id=style.Id,
                StyleName = style.Name
            };
            return viewModel;
        }

        public List<StyleViewModel> GetStylesViewModels(IReadOnlyCollection<Style> styles)
        {
            return styles.Select(s => new StyleViewModel()
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }
    }
}
