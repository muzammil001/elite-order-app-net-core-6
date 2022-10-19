using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Extensions;
using EliteOrderApp.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;
        private readonly IMapper _mapper;


        public ItemsController(ItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetItems")]
        public async Task<IActionResult> GetCustomers()
        {
            var list = await _itemService.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _itemService.GetItem(id);
            if (item == null)
            {
                return NotFound("Item not found.");
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(ItemDto itemDto)
        {

            if (!TryValidateModel(itemDto))
                return BadRequest(ModelState.GetFullErrorMessage());

            var item = _mapper.Map<Item>(itemDto);
            await _itemService.NewItem(item);
            return Ok(item);
        }

        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem(ItemDto itemDto)
        {
            var itemInDb = await _itemService.GetItem(itemDto.Id);
            if (itemInDb == null)
            {
                return NotFound("Item not found.");
            }
            _mapper.Map(itemDto, itemInDb);
            _itemService.UpdateItem();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItem(id);
            return NoContent();
        }
    }
}
