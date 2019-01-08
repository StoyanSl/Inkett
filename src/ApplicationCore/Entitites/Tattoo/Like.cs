

using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
    public class Like : BaseEntity
    {
        public Like()
        {

        }

        public Like(int profileId, int tattooId)
        {
            ProfileId = profileId;
            TattooId = tattooId;
        }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        public int TattooId { get; set; }

        public Tattoo Tattoo { get; set; }
    }
}
