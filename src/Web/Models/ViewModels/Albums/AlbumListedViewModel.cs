using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;

namespace Inkett.Web.Models.ViewModels.Albums
{
    public class AlbumListedViewModel : IMapFrom<Album>
    {
        public int Id { get; set;}

        public string Title { get; set; }

        public string PictureUri { get; set; }
    }
}
