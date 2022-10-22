using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CartService _cartService;
        public CartItemsController(IMapper mapper, CartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpGet]
        [Route("GetCartItems")]
        public async Task<IActionResult> GetCartItems()
        {
            var list = await _cartService.GetAll();
            return Ok(list);
        }


        [HttpGet]
        [Route("GetCartItem/{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var item = await _cartService.GetItem(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }
            return Ok(item);
        }


        [HttpPost]
        [Route("AddItemInCart")]
        public async Task<IActionResult> AddItemInCart(CartDto cartDto)
        {
            if (await _cartService.IsItemExists(cartDto.ItemId))
            {
                return BadRequest("Item is already exists.");
            }

            if (!TryValidateModel(cartDto))
                return BadRequest(ModelState.GetFullErrorMessage());

            var item = _mapper.Map<Cart>(cartDto);
            await _cartService.AddItemInCart(item);
            return Ok(item);
        }

        [HttpDelete]
        [Route("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _cartService.DeleteCartItem(id);
            return NoContent();
        }
    }
}
