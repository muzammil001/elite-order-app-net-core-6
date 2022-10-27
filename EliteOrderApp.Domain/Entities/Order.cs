namespace EliteOrderApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TotalAmount { get; set; }
        public int Discount { get; set; }

        public bool IsPending { get; set; }
        public bool IsCompleted { get; set; }

        public int AdvancePayment { get; set; }
    }
}
