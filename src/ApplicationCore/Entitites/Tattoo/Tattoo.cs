using Inkett.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inkett.ApplicationCore.Entitites
{
    public class Tattoo : BaseEntity, IProfileAuthorizable
    {
        [Required]
        public string PictureUri { get; set; }

        public string Description { get; set; }

        public int? AlbumId { get; set; }
        public Album Album { get; set; }

        [Required]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Like> Likes { get; set; } = new List<Like>();

        public List<TattooStyle> TattooStyles { get; set; } = new List<TattooStyle>();
    }
}
