using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        public DateTime? BirthdayDate { get; set; }
    }
}
