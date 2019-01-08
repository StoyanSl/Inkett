using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public List<TattooStyle> TattooStyles { get; set; }
    }
}
