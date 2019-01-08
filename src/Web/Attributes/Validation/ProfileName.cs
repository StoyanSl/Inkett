using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Inkett.Web.Attributes.Validation
{
    public class ProfileName:ValidationAttribute
    {
     
        private readonly string IncorrectLength = "The profile name is either too short or too long.";
        private readonly  string InappropriateChars = "The profile name should contain only digtis, letters and whitespaces";
        private readonly string ExistingProfileName = "The profile name already exists";
        private readonly string EmptyString = "The profile name cannot be empty.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IProfileService)validationContext.GetService(typeof(IProfileService));
            var profileName = (string)value;

            if (String.IsNullOrWhiteSpace(profileName))
            {
                return new ValidationResult(this.EmptyString);
            }
            if (service.ProfileNameExists(profileName))
            {
                return new ValidationResult(this.ExistingProfileName);
            }
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
