using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileAlbumsViewModel
    {
        public ProfileViewModel ProfileViewModel { get; set; }

        public List<ProfileAlbumViewModel> ProfileAlbumViewModels { get; set; }
    }
}
