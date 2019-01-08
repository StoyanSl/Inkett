using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Album
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        [AlbumTitle]
        [Display(Name = "Album Title")]
        [Required]
        public string Title { get; set; }

        [ImageValidation]
        [Display(Name = "Album Picture")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Album Description")]
        public string Description { get; set; }

        public string PictureUri { get; set; }
        

    }
}
