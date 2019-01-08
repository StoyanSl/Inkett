using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class LikeBindingModel
    {
        [Required]
        public int TattooId { get; set; }
    }
}
