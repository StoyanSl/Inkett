using AutoMapper;
using Inkett.Web.Attributes.Validation;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels;
using Inkett.Web.Models.ViewModels.Tattoos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class TattooEditInputModel : IMapTo<TattooEditViewModel>
    {
        public string Description { get; set; }

        [AtLeastOneStyleRequired]
        public List<StyleCheckboxModel> StylesCheckBoxes { get; set; } = new List<StyleCheckboxModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        public string PictureUri { get; set; }

        [Required]
        public int Album { get; set; }


    }
}
