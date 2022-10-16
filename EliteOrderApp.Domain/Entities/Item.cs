using System.ComponentModel.DataAnnotations;

namespace EliteOrderApp.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
