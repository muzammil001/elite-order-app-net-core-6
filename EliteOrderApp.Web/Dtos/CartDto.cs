using System.ComponentModel.DataAnnotations;

namespace EliteOrderApp.Web.Dtos
{
    public class CartDto
    {
        public int Id { get; set; } = 0;

        [Required]
        public int ItemId { get; set; }
    }
}
