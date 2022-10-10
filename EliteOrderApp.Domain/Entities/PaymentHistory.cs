namespace EliteOrderApp.Domain.Entities
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public DateTime PaidDate { get; set; }
        public int PaidAmount { get; set; }

        public string Description { get; set; }
        //public int Balance { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
