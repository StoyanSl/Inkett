using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class StyleWithTattoosSpecification : BaseSpecification<Style>
    {
        public StyleWithTattoosSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.TattooStyles);
            AddInclude("TattooStyles.Tattoo");
        }
    }
}
