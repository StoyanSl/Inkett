using Inkett.Web.Viewmodels.Profile;
using Inkett.Web.Viewmodels.Tattoo;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Home
{
    public class HomeIndexViewModel
    {
        public List<ListedTattooViewModel> ListedTattooViewModels { get; set; } = new List<ListedTattooViewModel>();

        public List<ProfileViewModel> ProfileViewModels { get; set; } = new List<ProfileViewModel>();
    }
}
