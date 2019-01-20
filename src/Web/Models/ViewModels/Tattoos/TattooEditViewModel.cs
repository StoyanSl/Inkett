using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Styles;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Inkett.Web.Models.ViewModels.Tattoos
{
    public class TattooEditViewModel : IMapFrom<Tattoo>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public List<StyleCheckboxModel> StylesCheckBoxes { get; set; } = new List<StyleCheckboxModel>();

        public List<StyleViewModel> CheckedStyles { get; set; } = new List<StyleViewModel>();

        public List<SelectListItem> Albums { get; set; } = new List<SelectListItem>();

        public string PictureUri { get; set; }

        public int Album { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, SelectListItem>()
               .ForMember(x => x.Value, m => m.MapFrom(a => a.Id.ToString()))
               .ForMember(x => x.Text, m => m.MapFrom(a => a.Title));

            configuration.CreateMap<Tattoo, TattooEditViewModel>()
              .ForMember(x => x.Album, m => m.MapFrom(t => t.AlbumId ?? 0))
              .ForMember(x => x.Albums, m => m.MapFrom(t => t.Profile.Albums))
              .ForMember(x => x.CheckedStyles, m => m.MapFrom(t => t.TattooStyles.Select(x => x.Style).ToList()))
              .AfterMap((s, d) => d.Albums.Add(new SelectListItem() { Value = "0", Text = "None", Selected = true }));
        }
    }
}
