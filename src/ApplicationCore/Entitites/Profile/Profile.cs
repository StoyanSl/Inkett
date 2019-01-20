using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
    public class Profile:BaseEntity
    {
        
        private const string defaultCover = "https://res.cloudinary.com/inkettimgs/image/upload/v1546455039/u72isnupu4bujqzb4c4u.png";
        private const string defaultProfile = "https://res.cloudinary.com/inkettimgs/image/upload/v1546454758/c9eeekpbmtrzkmakndu1.png";
        public Profile()
        {

        }
        public Profile(string accountId,string profileName, string profileDescription)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Guard.Against.NullOrEmpty(profileName, nameof(profileName));

            this.AccountId = accountId;
            this.Name = profileName;
            this.Description = profileDescription;
            this.ProfilePictureUri =defaultProfile;
            this.CoverPictureUri = defaultCover;
        }
        [Required]
        public string AccountId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public string ProfilePictureUri { get; set; }

        [Required]
        public string CoverPictureUri { get; set; }

        public string Description { get; set; }

        public List<Album> Albums { get; set; }

        public List<Tattoo> Tattoos { get; set; }

        public List<Like> Likes { get; set; }

        public List<Follow> Followers { get; set; }

        public List<Follow> Follows { get; set;  }

        public List<Notification> Notifications { get; set; }
    }
}
