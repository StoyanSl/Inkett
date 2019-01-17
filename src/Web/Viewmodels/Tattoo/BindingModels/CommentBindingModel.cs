using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class CommentBindingModel
    {
        [Required]
        [StringLength(maximumLength:255,MinimumLength =1)]
        public string CommentText { get; set; }

        [Required]
        public int TattooId { get; set; }
    }
}
