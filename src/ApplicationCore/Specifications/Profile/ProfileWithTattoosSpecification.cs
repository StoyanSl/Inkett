using Inkett.ApplicationCore.Entitites;

namespace Inkett.ApplicationCore.Specifications
{
    public class ProfileWithTattoosSpecification : BaseSpecification<Profile>
    {
        public ProfileWithTattoosSpecification(int profileId)
           : base(profile => profile.Id == profileId)
        {
            AddInclude(profile => profile.Tattoos);
            AddInclude(profile => profile.Followers);
            AddInclude($"Followers.Profile");
        }
    }
}
