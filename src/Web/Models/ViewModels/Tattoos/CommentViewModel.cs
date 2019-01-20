using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;
using Inkett.Web.Models.ViewModels.Profiles;

namespace Inkett.Web.Models.ViewModels.Tattoos
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public ProfileViewModel Profile { get; set; }

        public string Text { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                 .ForMember(x=>x.Profile,m=>m.MapFrom(c=>c.Profile));
        }
    }
}
