using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View("NewOrder");
        }
    }
}
