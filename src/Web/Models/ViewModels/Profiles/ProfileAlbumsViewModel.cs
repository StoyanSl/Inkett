using AutoMapper;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Albums;
using System.Collections.Generic;
using Profile = Inkett.ApplicationCore.Entitites.Profile;

namespace Inkett.Web.Models.ViewModels.Profiles
{
    public class ProfileAlbumsViewModel : IMapFrom<Profile>, IHaveCustomMappings
    {
        public ProfileViewModel Profile { get; set; }

        public List<AlbumListedViewModel> Albums { get; set; } = new List<AlbumListedViewModel>();

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Profile, ProfileAlbumsViewModel>()
                .ForMember(x => x.Profile, m => m.MapFrom(p=>p))
            .ForMember(x => x.Albums, m => m.MapFrom(p => p.Albums));
        }
    }
}
