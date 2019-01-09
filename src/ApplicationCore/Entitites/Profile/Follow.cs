using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
   public class Follow:BaseEntity
    {
        public Follow()
        {

        }

        public Follow(int profileId, int followedId)
        {
            ProfileId = profileId;
            FollowedProfileId = followedId;
        }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        public int FollowedProfileId { get; set; }

        public Profile FollowedProfile { get; set; }
    }
}
