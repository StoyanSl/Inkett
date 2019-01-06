using Inkett.Web.Viewmodels.Album;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileAlbumsViewModel
    {
        public ProfileAlbumsViewModel()
        {
            ProfileAlbums = new List<AlbumViewModel>();
        }
        public ProfileViewModel Profile { get; set; }

        public List<AlbumViewModel> ProfileAlbums { get; set; } 

        public bool IsOwner { get; set; }
    }
}
