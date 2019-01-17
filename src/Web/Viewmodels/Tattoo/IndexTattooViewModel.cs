using Inkett.ApplicationCore.Interfaces;
using Inkett.Web.Viewmodels.Profile;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class IndexTattooViewModel
    {
        
        public IndexTattooViewModel()
        {
            this.Styles = new List<StyleViewModel>();
        }

        public int Id { get; set; }

        public ProfileViewModel Profile { get; set; }

        public string Description { get; set; }

        public List<StyleViewModel> Styles { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public bool IsLiked { get; set; }

        public bool IsOwner { get; set; }

        public string PictureUri { get; set; }
        
    }
}
