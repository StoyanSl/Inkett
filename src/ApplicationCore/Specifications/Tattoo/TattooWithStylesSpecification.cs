using Inkett.ApplicationCore.Entitites;
using System.Collections.Generic;

namespace Inkett.ApplicationCore.Specifications
{
    class TattooWithStylesSpecification : BaseSpecification<Tattoo>
    {
        public TattooWithStylesSpecification(int tattooId) : base(t => t.Id == tattooId)
        {
            AddInclude(t => t.TattooStyles);
            AddInclude(t => t.Profile);
            AddInclude(t => t.Profile.Albums);
            AddInclude($"Comments.Profile");
            AddInclude(t=>t.Likes);
            
        }

    }
}
