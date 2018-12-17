using Ardalis.GuardClauses;

namespace Inkett.ApplicationCore.Entitites.Profile
{
    public class Profile:BaseEntity
    {
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
            this.ProfilePicture = string.Empty;
        }
        public string AccountId { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public string CoverPicture { get; set; }

        public string ProfileDescription { get; set; }
    }
}
