using EliteOrderApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetItems()
        {
            var items = _itemService.GetAll().ToList();
            return Ok(items);
        }

    }
}
