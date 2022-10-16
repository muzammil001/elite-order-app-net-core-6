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

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
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

    }
}
