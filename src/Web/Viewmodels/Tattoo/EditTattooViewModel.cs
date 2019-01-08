using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class EditTattooViewModel
    {
        public string Description { get; set; }

        [AtLeastOneStyleRequired]
        public List<CheckboxModel> StylesCheckBoxes { get; set; } = new List<CheckboxModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        public string PictureUri { get; set; }

        [Required]
        public int Album { get; set; }
    }
}
