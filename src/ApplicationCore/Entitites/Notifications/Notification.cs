using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{ 
    public class Notification : BaseEntity
    {
        public Notification()
        {
                
        }
        public Notification(int profileId,string pictureUri, string reference, string message)
        {
            ProfileId = profileId;
            PictureUri = pictureUri;
            Reference = reference;
            Message = message;
        }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        public string PictureUri { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsChecked { get; set; }
    }
}
