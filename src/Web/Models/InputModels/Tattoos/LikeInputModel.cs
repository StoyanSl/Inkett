using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class LikeInputModel
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int TattooId { get; set; }
    }
}
