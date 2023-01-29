using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using EliteOrderApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EliteOrderApp.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly PaymentService _paymentService;
        private readonly IMapper _mapper;


        public OrdersController(OrderService orderService, IMapper mapper, PaymentService paymentService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _paymentService = paymentService;
        }


        [HttpGet]
        [Route("get-pending-orders")]
        public async Task<IActionResult> GetPendingOrders()
        {
            var list = await _orderService.GetAllPendingOrders();
            var model = list.Select(x => new OrderListModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                DeliveryDate = x.DeliveryDate,
                CustomerName = $"{x.Customer.Name} - {x.Customer.Contact}",
                CustomerId = x.CustomerId,
                Balance= _paymentService.GetOrderBalance(x.Id),
                TotalAmount=x.TotalAmount,
                ReceivedAmount = _paymentService.GetReceivedAmount(x.Id)
            });
            return Ok(model);
        }

        [HttpGet]
        [Route("get-completed-orders")]
        public async Task<IActionResult> GetCompletedOrders()
        {
            var list = await _orderService.GetAllCompletedOrders();
            var model = list.Select( x => new OrderListModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                DeliveryDate = x.DeliveryDate,
                CustomerName = $"{x.Customer.Name} - {x.Customer.Contact}",
                Balance = _paymentService.GetOrderBalance(x.Id),
                TotalAmount = x.TotalAmount,
            });
            return Ok(model);
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
        [Route("complete-order/{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            await _orderService.CompleteOrder(id);
            return NoContent();
        }
        [HttpGet]
        [Route("pending-order/{id}")]
        public async Task<IActionResult> PendingOrder(int id)
        {
            await _orderService.PendingOrder(id);
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

            if (await _orderService.IsItemExists(orderDetailDto.ItemId, orderDetailDto.OrderId))
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
