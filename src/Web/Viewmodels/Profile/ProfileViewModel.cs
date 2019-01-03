using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Profile Name")]
        public string ProfileName { get; set; }

        [DataType(DataType.Text)]
        public string ProfileDescription { get; set; }

        public string ProfilePictureUri { get; set; }

        public string CoverPictureUri { get; set; }

    }
}
