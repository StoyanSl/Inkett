using Inkett.Web.Viewmodels.Tattoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileTattoosViewModel
    {
        public ProfileTattoosViewModel()
        {
            ProfileTattoos = new List<ListedTattooViewModel>();
        }
        public ProfileViewModel ProfileViewModel { get; set; }

        public List<ListedTattooViewModel> ProfileTattoos { get; set; }

        public bool IsOwner { get; set;  }
    }
}
