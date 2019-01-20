using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;

namespace Inkett.Web.Models.ViewModels.Styles
{
    public class StyleViewModel:IMapFrom<Style>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
