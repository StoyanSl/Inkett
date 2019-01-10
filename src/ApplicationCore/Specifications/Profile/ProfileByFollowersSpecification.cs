using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    class ProfileByFollowersSpecification:BaseSpecification<Profile>
    {
        public ProfileByFollowersSpecification(int skip, int take)
            :base(null)
        {
            ApplyOrderByDescending(p=> p.Followers.Count);
            ApplyPaging(skip, take);
        }
    }
}
