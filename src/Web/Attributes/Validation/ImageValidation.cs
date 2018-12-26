using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Inkett.Web.Attributes.Validation
{
    public class ImageValidation:ValidationAttribute
    {
        private const string ImageJpeg = "image/jpeg";
        private const string ImagePng = "image/png";
        private const string FileContentError = "Invalid file format.";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null)
            {
                return ValidationResult.Success;
            }
            var file = (IFormFile)value;

            if (file.ContentType!=ImageJpeg && file.ContentType != ImagePng)
            {
                return new ValidationResult(FileContentError);
            }
            return ValidationResult.Success;
        }
    }
}
