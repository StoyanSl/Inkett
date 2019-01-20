using AutoMapper;

namespace Inkett.Web.Common.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
