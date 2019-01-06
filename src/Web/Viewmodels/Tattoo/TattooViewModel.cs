using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class TattooViewModel
    {
        public string Description { get; set; }

        [Required]
        public IFormFile TattooPicture { get; set; }

        [AtLeastOneStyleRequired]
        public List<CheckboxModel> StylesCheckBoxes { get; set; } = new List<CheckboxModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        public string PictureUri { get; set; }

        public int Album { get; set; }

    }
}
