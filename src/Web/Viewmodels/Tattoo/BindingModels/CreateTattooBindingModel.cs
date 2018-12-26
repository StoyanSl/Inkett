using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Inkett.Web.Viewmodels.Tattoo.BindingModels
{
    public class CreateTattooBindingModel
    {
        public string Description { get; set; }

        public IFormFile TattooPicture { get; set; }

        [AtLeastOneStyleRequired]
        public List<CheckboxModel> StylesCheckBoxes { get; set; }
    }
}
