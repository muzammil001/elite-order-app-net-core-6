using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Dtos
{
	public class PaymentDto
	{
        public int Id { get; set; }
        public DateTime PaidDate { get; set; }
        public int PaidAmount { get; set; }

        public string Description { get; set; }

        public int OrderId { get; set; }
    }
}
