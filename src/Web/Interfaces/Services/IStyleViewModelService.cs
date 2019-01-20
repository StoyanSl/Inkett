using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Models.ViewModels.Styles;
using System.Collections.Generic;

namespace Inkett.Web.Interfaces.Services
{
    public interface IStyleViewModelService
    {
        List<StyleViewModel> GetStylesViewModels(IReadOnlyCollection<Style> styles);

        StyleIndexViewModel GetIndexStyleViewModel(IReadOnlyCollection<Tattoo> tattoos,int id,string styleName);
    }
}
