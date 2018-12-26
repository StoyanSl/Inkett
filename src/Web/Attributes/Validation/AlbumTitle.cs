using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Attributes.Validation
{
    public class AlbumTitle : ValidationAttribute
    {

        private readonly string IncorrectLength = "The album title is either too short or too long.";
        private readonly string InappropriateChars = "The album title should contain only digtis, letters and whitespaces.";
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var albumTitle = (string)value;
            if (String.IsNullOrWhiteSpace(albumTitle))
            {
                return new ValidationResult(this.IncorrectLength);
            }
            if (albumTitle.Length < 3 || albumTitle.Length > 25)
            {
                return new ValidationResult(this.IncorrectLength);
            }
            if (albumTitle.Any(x => !char.IsLetterOrDigit(x) && !char.IsWhiteSpace(x)))
            {
                return new ValidationResult(this.InappropriateChars);
            }

            return ValidationResult.Success;
        }
    }
}
