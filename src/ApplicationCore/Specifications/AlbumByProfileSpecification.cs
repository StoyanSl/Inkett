using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public class AlbumByProfileSpecification:BaseSpecification<Album>
    {
        public AlbumByProfileSpecification(int albumId, int profileId):base(a=> a.Id == albumId&&a.ProfileId==profileId)
        {
        }
        public AlbumByProfileSpecification(int profileId) : base(a=>a.ProfileId==profileId)
        {
        }
    }
}
