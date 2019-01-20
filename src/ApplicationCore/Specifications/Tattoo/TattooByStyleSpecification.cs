using Inkett.ApplicationCore.Entitites;
using System.Linq;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class TattooByStyleSpecification : BaseSpecification<Tattoo>
    {
        public TattooByStyleSpecification(int skip, int take, int styleId)
            : base(t => t.TattooStyles.Any(ts => ts.StyleId == styleId))
        {
            ApplyPaging(skip, take);
            AddInclude(t => t.TattooStyles);
            AddInclude("TattooStyles.Style");
        }
    }
}
