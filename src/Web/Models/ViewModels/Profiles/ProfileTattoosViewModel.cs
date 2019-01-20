using AutoMapper;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Collections.Generic;
using Profile = Inkett.ApplicationCore.Entitites.Profile;

namespace Inkett.Web.Models.ViewModels.Profiles
{
    public class ProfileTattoosViewModel : IMapFrom<Profile>, IHaveCustomMappings
    {
        public ProfileViewModel Profile { get; set; }

        public List<TattooListedViewModel> Tattoos { get; set; } = new List<TattooListedViewModel>();

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Profile, ProfileTattoosViewModel>()
               .ForMember(x => x.Profile, m => m.MapFrom(p => p))
            .ForMember(x => x.Tattoos, m => m.MapFrom(p => p.Tattoos));
        }
    }
}
