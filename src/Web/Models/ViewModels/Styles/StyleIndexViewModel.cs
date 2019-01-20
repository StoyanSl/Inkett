using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Collections.Generic;

namespace Inkett.Web.Models.ViewModels.Styles
{
    public class StyleIndexViewModel : IMapFrom<IReadOnlyCollection<Tattoo>>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TattooListedViewModel> Tattoos { get; set; } = new List<TattooListedViewModel>();

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IReadOnlyCollection<Tattoo>, StyleIndexViewModel>()
                .ForMember(x => x.Tattoos, m => m.MapFrom(t=>t));
        }
    }
}
