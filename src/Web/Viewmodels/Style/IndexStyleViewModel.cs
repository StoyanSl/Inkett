using Inkett.Web.Viewmodels.Tattoo;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Style
{
    public class IndexStyleViewModel
    {
        public int Id { get; set; }

        public string StyleName { get; set; }

        public List<ListedTattooViewModel> Tattoos { get; set; } = new List<ListedTattooViewModel>();
    }
}
