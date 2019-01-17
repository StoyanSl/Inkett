using Inkett.ApplicationCore.Interfaces;
using Inkett.Web.Viewmodels.Profile;
using Inkett.Web.Viewmodels.Tattoo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Album
{
    public class AlbumIndexViewModel
    {
        public int Id { get; set; }

        public ProfileViewModel Profile { get; set; }

        [Display(Name = "Album Title")]
        public string Title { get; set; }

        public List<ListedTattooViewModel> Tattoos { get; set; } = new List<ListedTattooViewModel>();

        public string Description { get; set; }

        public bool isOwner = false;
    }
}
