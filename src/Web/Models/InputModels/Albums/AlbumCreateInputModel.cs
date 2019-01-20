using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Albums
{
    public class AlbumCreateInputModel
    {
        [Display(Name = "Album Title")]
        [AlbumTitle]
        public string Title { get; set; }

        [Display(Name = "Album Picture")]
        [ImageValidation]
        public IFormFile Picture { get; set; }

        [Display(Name = "Album Description")]
        [MinLength(0)]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
