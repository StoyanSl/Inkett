using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class TattooByOrderSpecification : BaseSpecification<Tattoo>
    {
        public TattooByOrderSpecification(int skip, int take) : base(null)
        {
            ApplyPaging(skip, take);
        }
    }
}
