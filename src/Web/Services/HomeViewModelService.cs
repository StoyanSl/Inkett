using System.Collections.Generic;
using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.ViewModels.Home;
using Inkett.Web.Models.ViewModels.Profiles;
using Inkett.Web.Models.ViewModels.Tattoos;
using Profile = Inkett.ApplicationCore.Entitites.Profile;

namespace Inkett.Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        public HomeIndexViewModel GetHomeIndexViewModel(IReadOnlyCollection<Profile> profiles, IReadOnlyCollection<Tattoo> tattoos)
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.TattoosListedViewModels = Mapper.Map<IReadOnlyCollection<Tattoo>, List<TattooListedViewModel>>(tattoos);
            viewModel.ProfileViewModels = Mapper.Map<IReadOnlyCollection<Profile>, List<ProfileViewModel>>(profiles);

            return viewModel;
        }
    }
}
