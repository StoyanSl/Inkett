using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Entitites
{
    public class TattooStyle : BaseEntity
    {
        public Tattoo Tattoo { get; set; }
        public int TattooId { get; set; }

        public Style Style { get; set; }
        public int StyleId { get; set; }
    }
}
