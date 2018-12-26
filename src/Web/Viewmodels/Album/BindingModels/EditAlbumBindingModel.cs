using Inkett.Web.Attributes.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Album.BindingModels
{
    public class EditAlbumBindingModel
    {
        public string Title { get; set; }

        [ImageValidation]
        [Display(Name = "Album Picture")]
        public IFormFile AlbumPicture { get; set; }

        public string Description { get; set; }
    }
}
