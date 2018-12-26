using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Inkett.ApplicationCore.Entitites
{
    public class Profile:BaseEntity
    {
        private const string defaultCover = "https://res.cloudinary.com/inkettimgs/image/upload/v1545262005/dzibhpjdcs7fatuoqsgg.png";
        private const string defaultProfile = "https://res.cloudinary.com/inkettimgs/image/upload/v1545262005/kuftk6oeg691tsboqibv.jpg";
        public Profile()
        {

        }
        public Profile(string accountId,string profileName, string profileDescription)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Guard.Against.NullOrEmpty(profileName, nameof(profileName));

            this.AccountId = accountId;
            this.ProfileName = profileName;
            this.ProfileDescription = profileDescription;
            this.ProfilePicture =defaultProfile;
            this.CoverPicture = defaultCover;
        }
        public string AccountId { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public string CoverPicture { get; set; }

        public string ProfileDescription { get; set; }

        public List<Album> Albums { get; set; }

        public List<Tattoo> Tattoos { get; set; }
    }
}
