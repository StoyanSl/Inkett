using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inkett.ApplicationCore.Entitites
{
    public class Style : BaseEntity
    {
        public Style()
        {

        }
        public Style(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        [NotMapped]
        public List<TattooStyle> TattooStyles { get; set; }
    }
}
