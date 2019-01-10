using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class ProfileWithAlbumsSpecification:BaseSpecification<Profile>
    {
        public ProfileWithAlbumsSpecification(int profileId)
            : base(profile => profile.Id == profileId)
        {
            AddInclude(profile => profile.Albums);
            AddInclude(profile => profile.Followers);
        }
    }
}
