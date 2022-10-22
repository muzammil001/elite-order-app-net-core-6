using System.ComponentModel.DataAnnotations;

namespace EliteOrderApp.Web.Dtos
{
	public class CustomerDto
	{
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Contact { get; set; }
    }
}
