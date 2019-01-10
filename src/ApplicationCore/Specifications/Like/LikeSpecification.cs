using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class LikeSpecification : BaseSpecification<Like>
    {
        public LikeSpecification( int profileId, int tattooId) :base(l=>l.ProfileId==profileId && l.TattooId==tattooId)
        {

        }
    }
}
