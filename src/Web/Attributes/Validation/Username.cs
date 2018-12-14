﻿using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Inkett.Web.Attributes.Validation
{
    public class ProfileName:ValidationAttribute
    {
        private readonly string IncorrectLength = "The profile name is either too short or too long.";
        private readonly  string InappropriateChars = "The profile name should contain only digtis, letters and whitespaces";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var profileName = (string)value;
            if (profileName.Length<2 || profileName.Length>25)
            {
                return new ValidationResult(this.IncorrectLength);
            }
            if (profileName.Any(x=>!char.IsLetterOrDigit(x)&&!char.IsWhiteSpace(x)))
            {
                return new ValidationResult(this.InappropriateChars);
            }
            return ValidationResult.Success;
        }
    }
}
