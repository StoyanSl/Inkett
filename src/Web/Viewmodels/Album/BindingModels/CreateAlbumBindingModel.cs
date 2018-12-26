using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Album.BindingModels
{
    public class CreateAlbumBindingModel
    {
        [AlbumTitle]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        [ImageValidation]
        [Display(Name = "Album Picture")]
        public IFormFile AlbumPicture { get; set; }

        [Display(Name = "Album Description")]
        public string Description { get; set; }
    }
}
