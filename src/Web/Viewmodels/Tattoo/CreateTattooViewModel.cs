using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class CreateTattooViewModel
    {
        public string Description { get; set; }

        public IFormFile TattooPicture { get; set; }

        [AtLeastOneStyleRequired]
        public List<CheckboxModel> StylesCheckBoxes { get; set; } = new List<CheckboxModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        public int Album { get; set; }
    }
}
