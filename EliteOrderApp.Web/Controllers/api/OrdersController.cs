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
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;


        public OrdersController(OrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("get-pending-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var list = await _orderService.GetAllPendingOrders();
            return Ok(list);
        }

       

        [HttpPut]
        [Route("update-order")]
        public async Task<IActionResult> UpdateOrder(OrderDto orderDto)
        {
            var orderInDb = await _orderService.GetOrder(orderDto.Id);
           
            var order = _mapper.Map(orderDto, orderInDb);
            _orderService.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-order/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return NoContent();
        }

        [HttpGet]
        [Route("get-orders-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var list = await _orderService.GetOrderDetails(orderId);
            return Ok(list);
        }
        [HttpPost]
        [Route("add-order-item")]
        public async Task<IActionResult> AddItem(OrderDetailDto orderDetailDto)
        {
            if (orderDetailDto.ItemId == 0)
            {
                return BadRequest("Please select Item.");
            }

            if (await _orderService.IsItemExists(orderDetailDto.ItemId,orderDetailDto.OrderId))
            {
                return BadRequest("Item is already exists.");
            }

            if (!TryValidateModel(orderDetailDto))
                return BadRequest(ModelState.GetFullErrorMessage());

            var item = _mapper.Map<OrderDetail>(orderDetailDto);
            await _orderService.AddOrderItem(item);
            return Ok(item);
        }

        [HttpDelete]
        [Route("delete-order-detail-item/{id}")]
        public async Task<IActionResult> DeleteOrderDetailItem(int id)
        {
            await _orderService.DeleteOrderDetailItem(id);
            return NoContent();
        }
    }
}
