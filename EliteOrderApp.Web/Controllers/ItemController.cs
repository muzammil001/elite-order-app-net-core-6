using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static EliteOrderApp.Web.Extensions.Helper;

namespace EliteOrderApp.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService _itemService;
        private readonly CartService _cartService;
        private readonly IMapper _mapper;

        public ItemController(ItemService itemService, CartService cartService, IMapper mapper)
        {
            _itemService = itemService;
            _cartService = cartService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            //Create form 
            if (id == 0)
            {
                return View(new ItemDto());

            }
            //edit form
            var item = await _itemService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            var itemDto = _mapper.Map<ItemDto>(item);

            return View(itemDto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {

                    var item = _mapper.Map<Item>(itemDto);
                    await _itemService.NewItem(item);
                }
                else
                {
                    try
                    {
                        var item = _mapper.Map<Item>(itemDto);
                        _itemService.UpdateItem(item);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return BadRequest();
                    }
                }
                return Json(new { isValid = true, html = "" });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", itemDto) });
        }
    }
}
