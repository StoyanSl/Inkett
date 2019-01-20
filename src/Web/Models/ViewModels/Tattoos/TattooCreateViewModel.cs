using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Inkett.Web.Models.ViewModels.Tattoos
{
    public class TattooCreateViewModel
    {
        public List<SelectListItem> AlbumsSelectList { get; set; } = new List<SelectListItem>();
    }
}
