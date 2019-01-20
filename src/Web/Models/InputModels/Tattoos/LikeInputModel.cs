using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class LikeInputModel
    {
        [Required]
        public int TattooId { get; set; }
    }
}
