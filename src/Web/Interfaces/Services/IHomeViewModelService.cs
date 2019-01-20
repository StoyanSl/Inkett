using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Models.ViewModels.Home;
using System.Collections.Generic;

namespace Inkett.Web.Interfaces.Services
{
    public interface IHomeViewModelService
    {
        HomeIndexViewModel GetHomeIndexViewModel(IReadOnlyCollection<Profile> profiles, IReadOnlyCollection<Tattoo> tattoos);
    }
}
