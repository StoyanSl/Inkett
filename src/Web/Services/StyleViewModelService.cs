using System.Collections.Generic;
using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.ViewModels.Styles;

namespace Inkett.Web.Services
{
    public class StyleViewModelService : IStyleViewModelService
    {
        public StyleIndexViewModel GetIndexStyleViewModel(IReadOnlyCollection<Tattoo> tattoos,int id ,string styleName)
        {
            var viewModel = Mapper.Map<IReadOnlyCollection<Tattoo>, StyleIndexViewModel>(tattoos);
            viewModel.Id = id;
            viewModel.Name = styleName;
            return viewModel;
        }

        public List<StyleViewModel> GetStylesViewModels(IReadOnlyCollection<Style> styles)
        {
            return Mapper.Map<IReadOnlyCollection<Style>, List<StyleViewModel>>(styles);
        }
    }
}
