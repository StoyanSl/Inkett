using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Profile
{
    public class EditProfileViewModel
    {
        [Display(Name = "Profile Name")]
        public string ProfileName { get; set; }

        public string ProfilePictureUri { get; set; }

        public string CoverPictureUri { get; set; }

    }
}
