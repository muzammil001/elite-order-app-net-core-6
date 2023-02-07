namespace EliteOrderApp.Web.Models;

public class DashboardViewModel
{
    public int TotalPendingOrders { get; set; }
    public int TotalUpcomingDeliveries { get; set; }
    public int RevenueByYear { get; set; }
    public int CashRecovery { get; set; }
}