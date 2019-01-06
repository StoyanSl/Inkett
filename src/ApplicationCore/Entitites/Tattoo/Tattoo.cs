using Inkett.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace Inkett.ApplicationCore.Entitites
{
    public class Tattoo : BaseEntity,IProfileAuthorizable
    {
        public string TattooPictureUri { get; set; }

        public string Description { get; set; }

        public int? AlbumId { get; set; }
        public Album Album { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Like> Likes { get; set; } = new List<Like>();

        public List<TattooStyle> TattooStyles { get; set; } = new List<TattooStyle>();
    }
}
