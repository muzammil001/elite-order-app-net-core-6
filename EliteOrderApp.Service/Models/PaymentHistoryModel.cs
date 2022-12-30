using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteOrderApp.Service.Models
{
    public class PaymentHistoryModel
    {
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
