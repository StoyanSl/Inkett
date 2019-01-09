using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

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
