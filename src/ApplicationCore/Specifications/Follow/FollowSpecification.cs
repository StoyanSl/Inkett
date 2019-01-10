using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class FollowSpecification : BaseSpecification<Follow>
    {
        public FollowSpecification(int followerId, int followedId)
            : base(f => f.ProfileId== followerId && f.FollowedProfileId== followedId)
        {

        }
    }
}
