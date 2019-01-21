using Inkett.Web.Attributes.Validation;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class TattooEditInputModel : IMapTo<TattooEditViewModel>
    {
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [AtLeastOneStyleRequired]
        public List<StyleCheckboxModel> StylesCheckBoxes { get; set; } = new List<StyleCheckboxModel>();
        
        [DataType(DataType.Url)]
        public string PictureUri { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Album { get; set; }


    }
}
