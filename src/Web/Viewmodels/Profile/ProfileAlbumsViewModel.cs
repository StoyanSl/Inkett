using Inkett.Web.Viewmodels.Album;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileAlbumsViewModel
    {
        public ProfileAlbumsViewModel()
        {
            ProfileAlbumViewModels = new List<AlbumViewModel>();
        }
        public ProfileViewModel ProfileViewModel { get; set; }

        public List<AlbumViewModel> ProfileAlbumViewModels { get; set; } 

        public bool IsOwner { get; set; }
    }
}
