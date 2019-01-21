using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Albums
{
    public class AlbumDeleteInputModel
    {
        [Range(0,int.MaxValue)]
        public int Id { get; set; }
    }
}
