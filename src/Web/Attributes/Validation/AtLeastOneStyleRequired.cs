using Inkett.Web.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Inkett.Web.Attributes.Validation
{
    public class AtLeastOneStyleRequired : ValidationAttribute
    {
        private const string atleastOneError = "Atleast one Style must be selected.";
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var checkBoxes = (List<StyleCheckboxModel>)value;
            if (checkBoxes.Any(v => v.Checked))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(atleastOneError);
        }
    }
}
