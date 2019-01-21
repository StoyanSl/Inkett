using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Profiles
{
    public class ProfileEditInputModel
    {
        [ImageValidation]
        public IFormFile CoverPictureFile { get; set; }

        [ImageValidation]
        public IFormFile ProfilePictureFile { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
