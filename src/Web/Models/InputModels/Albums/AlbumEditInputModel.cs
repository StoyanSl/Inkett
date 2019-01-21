using AutoMapper;
using Inkett.Web.Attributes.Validation;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Albums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Albums
{
    public class AlbumEditInputModel : IMapTo<AlbumEditViewModel>
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [AlbumTitle]
        [Display(Name = "Album Title")]
        public string Title { get; set; }

        [ImageValidation]
        [Display(Name = "Album Picture")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Album Description")]
        public string Description { get; set; }
        
        [DataType(DataType.Url)]
        public string PictureUri { get; set; }
    }
}
