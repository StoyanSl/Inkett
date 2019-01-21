using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Attributes.Validation;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Profile = Inkett.ApplicationCore.Entitites.Profile;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class TattooCreateInputModel : IMapFrom<Profile>, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Tattoo Picture")]
        public IFormFile TattooPicture { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [AtLeastOneStyleRequired]
        public List<StyleCheckboxModel> StylesCheckBoxes { get; set; } = new List<StyleCheckboxModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        [Required]
        [Range(0,int.MaxValue)]
        public int Album { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, SelectListItem>()
                .ForMember(x => x.Value, m => m.MapFrom(a => a.Id.ToString()))
                .ForMember(x => x.Text, m => m.MapFrom(a => a.Title));

            configuration.CreateMap<Profile, TattooCreateInputModel>()
                .ForMember(x => x.Albums, m => m.MapFrom(p => p.Albums))
                .ForMember(x => x.Description, m => m.Ignore())
                .AfterMap((s, d) => d.Albums.Add(new SelectListItem() { Value = "0", Text = "None", Selected = true }));
            
        }
    }
}
