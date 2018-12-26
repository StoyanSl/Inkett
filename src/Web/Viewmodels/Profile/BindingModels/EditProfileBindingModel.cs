using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Viewmodels.Profile.BindingModels
{
    public class EditProfileBindingModel
    {
        [ImageValidation]
        public IFormFile CoverPictureFile { get; set; }

        [ImageValidation]
        public IFormFile ProfilePictureFile { get; set; }
    }
}
