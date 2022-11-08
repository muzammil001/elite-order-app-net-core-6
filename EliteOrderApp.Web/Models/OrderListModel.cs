namespace EliteOrderApp.Web.Models
{
	public class OrderListModel
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public string CustomerName { get; set; }
		public int Balance { get; set; }
		public int TotalAmount { get; set; }
	}
}
