using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Web.Dtos;

namespace EliteOrderApp.Web.Models
{
    public class PaymentTrackingModel
    {
        public Order Order { get; set; }
        public int Balance { get; set; }
    }
}
