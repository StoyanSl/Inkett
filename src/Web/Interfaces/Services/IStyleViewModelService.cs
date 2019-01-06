using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Style;
using Inkett.Web.Viewmodels.Tattoo;
using System.Collections.Generic;

namespace Inkett.Web.Interfaces.Services
{
    public interface IStyleViewModelService
    {
        List<StyleViewModel> GetStylesViewModels(IReadOnlyCollection<Style> styles);
        IndexStyleViewModel GetIndexStyleViewModel(Style style);
    }
}
