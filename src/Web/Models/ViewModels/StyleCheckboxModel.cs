using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;

namespace Inkett.Web.Models.ViewModels
{
    public class StyleCheckboxModel:IMapFrom<Style>,IHaveCustomMappings
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Style, StyleCheckboxModel>()
                .ForMember(x => x.Value, m => m.MapFrom(s => s.Id))
                .ForMember(x => x.Text, m => m.MapFrom(s => s.Name));
        }
    }
}
