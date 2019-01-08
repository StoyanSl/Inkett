using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
   public class Follow:BaseEntity
    {
        public Follow()
        {

        }

        public Follow(int followerId, int followedId)
        {
            FollowerId = followerId;
            FoollowedId = followedId;
        }

        [Required]
        public int FollowerId { get; set; }

        public Profile FollowerProfile { get; set; }

        [Required]
        public int FoollowedId { get; set; }

        public Profile FollowedProfile { get; set; }
    }
}
