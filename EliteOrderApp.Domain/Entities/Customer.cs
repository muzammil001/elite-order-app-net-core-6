using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EliteOrderApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Contact { get; set; }
    }
}
