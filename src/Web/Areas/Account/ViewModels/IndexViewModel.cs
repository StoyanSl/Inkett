using System;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Areas.Account.ViewModels
{
    public class IndexViewModel
    {

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "BirthDay")]
        public DateTime? BirthdayDate { get; set; } = DateTime.UtcNow;

        public string StatusMessage { get; set; }
    }
}
