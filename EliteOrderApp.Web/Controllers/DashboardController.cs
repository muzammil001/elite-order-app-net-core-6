using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
