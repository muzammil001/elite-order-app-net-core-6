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

        [HttpGet]
        public object GetItems(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_itemService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult InsertItem(string values)
        {
            var item = new Item();
            JsonConvert.PopulateObject(values, item);

            if (!TryValidateModel(item))
                return BadRequest(ModelState.GetFullErrorMessage());
            _itemService.NewItem(item);
            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(int key, string values)
        {
            var item = await _itemService.GetItem(key);
            JsonConvert.PopulateObject(values, item);

            if (!TryValidateModel(item))
                return BadRequest(ModelState.GetFullErrorMessage());

            _itemService.UpdateItem();
            return Ok(item);
        }

        [HttpDelete]
        public void DeleteItem(int key)
        {
            _itemService.DeleteItem(key);
        }


        #region Cart
        [HttpGet]
        public object ItemsLookup(DataSourceLoadOptions loadOptions)
        {
            var items = _itemService.GetAll();
            var lookup = items.Select(x => new
            {
                Value = x.Id,
                Text = x.Name
            });
            return DataSourceLoader.Load(lookup, loadOptions);
        }
        [HttpGet]
        public object GetCartItems(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_cartService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult AddItemInCart(string values)
        {
            var item = new Item();
            JsonConvert.PopulateObject(values, item);

            if (!TryValidateModel(item))
                return BadRequest(ModelState.GetFullErrorMessage());
            _cartService.AddItemInCart(item);
            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(int key, string values)
        {
            var item = await _cartService.GetItem(key);
            JsonConvert.PopulateObject(values, item);

            if (!TryValidateModel(item))
                return BadRequest(ModelState.GetFullErrorMessage());

            _cartService.UpdateCart();
            return Ok(item);
        }

        [HttpDelete]
        public void DeleteCartItem(int key)
        {
            _cartService.DeleteCartItem(key);
        }
        #endregion

    }
}
