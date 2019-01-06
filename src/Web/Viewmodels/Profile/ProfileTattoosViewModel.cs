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
            Tattoos = new List<ListedTattooViewModel>();
        }
        public ProfileViewModel Profile { get; set; }

        public List<ListedTattooViewModel> Tattoos { get; set; }

        public bool IsOwner { get; set;  }
    }
}
