using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Models
{
	public class OrderModel
	{
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int CustomerId { get; set; } 
        public int ItemId { get; set; }

        public int TotalAmount { get; set; }
        public int Discount { get; set; }

        public bool IsPending { get; set; }
        public bool IsCompleted { get; set; }

        public int AdvancePayment { get; set; }


    }
}
