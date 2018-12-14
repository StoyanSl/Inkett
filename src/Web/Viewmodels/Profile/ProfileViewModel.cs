﻿using Inkett.Web.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Profile
{
    public class ProfileViewModel
    {
        [Required]
        [ProfileName]
        [Display(Name = "Profile Name")]
        public string ProfileName { get; set; }
        
        [DataType(DataType.Text)]
        public string ProfileDescription { get; set; }
    }
}
