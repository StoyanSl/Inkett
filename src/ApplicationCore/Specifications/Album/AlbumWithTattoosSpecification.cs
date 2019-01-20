using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public class AlbumWithTattoosSpecification : BaseSpecification<Album>
    {
        public AlbumWithTattoosSpecification(int albumId) : base(a => a.Id == albumId)
        {
            AddInclude(a => a.Tattoos);
            AddInclude(a => a.Profile);
        }

    }
}
