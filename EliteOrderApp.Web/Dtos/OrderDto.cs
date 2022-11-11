using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Web.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [DisplayName("Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeliveryDate { get; set; }

        [DisplayName("Customers")]
        [Required(ErrorMessage = "Please select Customer")]
        public int? CustomerId { get; set; } = 0;
        public CustomerDto Customer { get; set; }
        
        [DisplayName("Items")]
        [Required(ErrorMessage = "Please select Item")]
        public int? ItemId { get; set; } = 0;

        public string TotalAmount { get; set; } = "0";
        public int Discount { get; set; }
        
        public string AdvancePayment { get; set; } = "0";
        public string Balance { get; set; } = "0";

        public bool IsPending { get; set; }
        public bool IsCompleted { get; set; }
     
    }
}
