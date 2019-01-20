using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
    public class Album : BaseEntity, IProfileAuthorizable
    {
        private const string defaultPictureUri = "https://res.cloudinary.com/inkettimgs/image/upload/v1545352616/rc7oscdwtwzpg6aheec8.jpg";
        private const string defaultAlbumTitle = "My Tattoos";
        private const string defaultAlbumDescription = "Tattoos that I currently have.";
        public Album()
        {

        }
        public Album(int profileId)
        {
            this.ProfileId = profileId;
            this.Title = defaultAlbumTitle;
            this.Description = defaultAlbumDescription;
            this.PictureUri = defaultPictureUri;
        }
        public Album(int profileId, string title, string description, string pictureUri = defaultPictureUri)
        {
            this.ProfileId = profileId;
            this.Title = title;
            this.Description = description;
            this.PictureUri = pictureUri ?? defaultPictureUri;
        }

        [Required]
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(255,MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        public string PictureUri { get; set; }

        public List<Tattoo> Tattoos { get; set; }
    }
}
