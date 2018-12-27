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

        public List<TattooStyle> TattooStyles { get; set; } = new List<TattooStyle>();
    }
}
