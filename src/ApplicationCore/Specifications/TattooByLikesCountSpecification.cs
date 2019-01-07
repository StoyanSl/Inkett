using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class TattooByLikesCountSpecification : BaseSpecification<Tattoo>
    {
        public TattooByLikesCountSpecification(int skip,int take):base(null)
        {
            ApplyOrderByDescending(t=>t.Likes.Count);
            ApplyPaging(skip,take);
        }
    }
}
