using Inkett.Web.Viewmodels.Profile;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class IndexTattooViewModel
    {
        public IndexTattooViewModel()
        {
            this.StylesViewModels = new List<StyleViewModel>();
        }
        public ProfileViewModel ProfileViewModel { get; set; }

        public string Description { get; set; }

        public List<StyleViewModel> StylesViewModels { get; set; }

        public string PictureUri { get; set; }
    }
}
