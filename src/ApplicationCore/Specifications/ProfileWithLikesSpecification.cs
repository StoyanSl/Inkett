using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class ProfileWithLikesSpecification : BaseSpecification<Profile>
    {
        public ProfileWithLikesSpecification(int profileId)
            : base(profile => profile.Id == profileId)
        {
            AddInclude(profile => profile.Likes);
            AddInclude("Likes.Tattoo");
            AddInclude(profile => profile.Followers);
        }
    }
}
