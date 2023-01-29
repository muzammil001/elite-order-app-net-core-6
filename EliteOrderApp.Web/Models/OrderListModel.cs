using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Models
{
	public class OrderListModel
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public string CustomerName { get; set; }
		public Customer Customer{ get; set; }
		public int Balance { get; set; }
		public int TotalAmount { get; set; }
		public int ReceivedAmount { get; set; }
	}
}
