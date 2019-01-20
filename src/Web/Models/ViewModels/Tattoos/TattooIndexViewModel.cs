using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Profiles;
using Inkett.Web.Models.ViewModels.Styles;
using System.Collections.Generic;

namespace Inkett.Web.Models.ViewModels.Tattoos
{
    public class TattooIndexViewModel:IMapFrom<Tattoo>
    {
        public int Id { get; set; }

        public ProfileViewModel Profile { get; set; }

        public string Description { get; set; }

        public List<StyleViewModel> Styles { get; set; } = new List<StyleViewModel>();

        public List<CommentViewModel> Comments { get; set; }

        public bool IsLiked { get; set; }

        public bool IsOwner { get; set; }

        public string PictureUri { get; set; }
    }
}
