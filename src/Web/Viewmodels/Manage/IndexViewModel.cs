using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Manage
{
    public class IndexViewModel
    {

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
