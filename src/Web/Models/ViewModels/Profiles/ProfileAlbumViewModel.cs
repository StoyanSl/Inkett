using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.ViewModels.Profiles
{
    public class ProfileAlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public ProfileViewModel Profile { get; set; }

        [Display(Name = "Album Title")]
        public string Title { get; set; }

        public List<TattooListedViewModel> Tattoos { get; set; } = new List<TattooListedViewModel>();

        public string Description { get; set; }

        public bool IsOwner { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, ProfileAlbumViewModel>()
                .ForMember(x => x.Profile, m => m.MapFrom(a => a.Profile))
                .ForMember(x => x.Tattoos, m => m.MapFrom(a => a.Tattoos));

        }
    }
}