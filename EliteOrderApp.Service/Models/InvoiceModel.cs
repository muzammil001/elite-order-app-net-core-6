using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteOrderApp.Service.Models
{
    public class InvoiceModel
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string ItemName { get; set; }
        public decimal TotalBill { get; set; }
        public decimal Balance { get; set; }
        public decimal AdvanceAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
