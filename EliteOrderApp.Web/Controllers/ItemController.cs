using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EliteOrderApp.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService _itemService;
        private readonly CartService  _cartService;

        public ItemController(ItemService itemService, CartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
