using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Dtos
{
	public class OrderDetailDto
	{
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int OrderId { get; set; }
    }
}
