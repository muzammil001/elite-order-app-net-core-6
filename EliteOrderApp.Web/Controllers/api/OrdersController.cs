using AutoMapper;
using EliteOrderApp.Service;
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

        [HttpGet]
        [Route("get-orders-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var list = await _orderService.GetOrderDetails(orderId);
            return Ok(list);
        }

    }
}
