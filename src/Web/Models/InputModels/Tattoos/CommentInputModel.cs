using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class CommentInputModel
    {
        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 1)]
        public string CommentText { get; set; }

        [Required]
        public int TattooId { get; set; }
    }
}
