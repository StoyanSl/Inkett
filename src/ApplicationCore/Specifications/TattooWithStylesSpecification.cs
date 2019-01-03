using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    class TattooWithStylesSpecification : BaseSpecification<Tattoo>
    {
        public TattooWithStylesSpecification(int tattooId) : base(t => t.Id == tattooId)
        {
            AddInclude(t => t.TattooStyles);
            AddInclude(t => t.Profile);
            AddInclude(t => t.Profile.Albums);
            //   AddInclude($"{nameof(Tattoo.Profile)}.{nameof(Profile.Albums)}");
        }

    }
}
