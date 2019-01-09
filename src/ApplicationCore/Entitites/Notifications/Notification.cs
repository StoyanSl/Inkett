using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites.Notifications
{
    public class Notification : BaseEntity
    {
        public Notification()
        {
                
        }
        public Notification(int profileId,string imageUri, string reference, string message)
        {
            ProfileId = profileId;
            ImageUri = imageUri;
            Reference = reference;
            Message = message;
        }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        public string ImageUri { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsChecked { get; set; }
    }
}
