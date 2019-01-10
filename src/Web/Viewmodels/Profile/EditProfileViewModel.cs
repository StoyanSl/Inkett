using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Profile
{
    public class EditProfileViewModel
    {

        public ProfileViewModel Profile { get; set; }

        [ImageValidation]
        public IFormFile CoverPictureFile { get; set; }

        [ImageValidation]
        public IFormFile ProfilePictureFile { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

    }
}
