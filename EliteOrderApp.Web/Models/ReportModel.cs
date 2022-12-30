using EliteOrderApp.Service.Models;

namespace EliteOrderApp.Web.Models
{
	public class ReportModel
	{
        public List<InvoiceModel> Invoice { get; set; }
        public List<PaymentHistoryModel> PaymentHistory { get; set; }

        public bool IsInvoice { get; set; }
        public string PageTitle { get; set; }
	}
}
