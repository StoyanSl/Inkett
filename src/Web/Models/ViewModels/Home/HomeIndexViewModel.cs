using Inkett.Web.Models.ViewModels.Profiles;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Collections.Generic;

namespace Inkett.Web.Models.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<TattooListedViewModel> TattoosListedViewModels { get; set; } = new List<TattooListedViewModel>();

        public List<ProfileViewModel> ProfileViewModels { get; set; } = new List<ProfileViewModel>();
    }
}
