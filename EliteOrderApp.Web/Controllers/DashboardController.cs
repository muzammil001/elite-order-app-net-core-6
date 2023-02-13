using EliteOrderApp.Service;
using EliteOrderApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel()
            {
                TotalPendingOrders = await _dashboardService.GetTotalPendingOrdersCount(),
                TotalUpcomingDeliveries = await _dashboardService.GetUpcomingDeliveriesCount(DateTime.Now),
                RevenueByYear= await _dashboardService.GetRevenueByYearSum(DateTime.Now),
                CashRecovery= await _dashboardService.GetTotalCashRecoverySum(),
                RevenueModel = await _dashboardService.GetOrderRevenue()
            };
            return View(model);
        }
    }
}
