using System.ComponentModel.DataAnnotations;

namespace Inkett.Web.Models.InputModels.Tattoos
{
    public class TattooDeleteInputModel
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
