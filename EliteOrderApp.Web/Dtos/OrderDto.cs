using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [DisplayName("Delivery Date")]
        public DateTime DeliveryDate { get; set; } = DateTime.Today;

        [DisplayName("Customers")]
        [Required(ErrorMessage = "Please select Customer")]
        public int? CustomerId { get; set; } = 0;

        [DisplayName("Items")]
        [Required(ErrorMessage = "Please select Item")]

        public int? ItemId { get; set; } = 0;

        public int TotalAmount { get; set; } = 0;
        public int Discount { get; set; }
        
        public int AdvancePayment { get; set; } = 0;
        public int Balance { get; set; } = 0;

        public bool IsPending { get; set; }
        public bool IsCompleted { get; set; }
     
    }
}
