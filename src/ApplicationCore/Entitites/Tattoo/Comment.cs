using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
    public class Comment:BaseEntity
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        public int TattooId { get; set; }

        public Tattoo Tattoo { get; set; }
    }
}
