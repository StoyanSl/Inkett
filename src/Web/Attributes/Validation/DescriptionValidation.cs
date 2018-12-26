using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Attributes.Validation
{
    public class DescriptionValidation : ValidationAttribute
    {

        private readonly string IncorrectLength = "The description is either too short or too long.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var description = (string)value;

            if (description.Length < 2 || description.Length > 225)
            {
                return new ValidationResult(this.IncorrectLength);
            }

            return ValidationResult.Success;
        }
    }
}
