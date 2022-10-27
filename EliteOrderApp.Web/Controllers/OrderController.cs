using AutoMapper;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Dtos;
using EliteOrderApp.Web.Extensions;
using EliteOrderApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EliteOrderApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly CartService _cartItemService;
        private readonly ItemService _itemService;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;

        private readonly IMapper _mapper;

        public OrderController(CartService cartItemService, CustomerService customerService, IMapper mapper, OrderService orderService, ItemService itemService)
        {
            _cartItemService = cartItemService;
            _customerService = customerService;
            _mapper = mapper;
            _orderService = orderService;
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            //clearing cart 
            _cartItemService.ClearCart();

            var items = await _itemService.GetAll();
            var customers = await _customerService.GetAll();
            var order = new OrderModel()
            {
                Order = new OrderDto(),
                Items = items.ToList(),
               Customers = customers.ToList()
            };
            return View("NewOrder", order);
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(OrderModel model)
        {
            if (model.Order.Id == 0)
            {
                if (!TryValidateModel(model.Order))
                    return BadRequest(ModelState.GetFullErrorMessage());


                var cartItems = await _cartItemService.GetAll();
                if (cartItems.Count == 0)
                {
                    return BadRequest("Please add items in cart.");
                }

                if (model.Order.TotalAmount==0)
                {
                    return BadRequest("Please enter order total amount.");

                }
                //Saving order
                var order = _mapper.Map<Order>(model.Order);
                var orderId = await _orderService.SaveOrder(order);

                //Saving order details
                var orderDetailsDto = _cartItemService.GetAll().Result.Select(x => new OrderDetailDto()
                {
                    ItemId = x.ItemId,
                    OrderId = orderId
                }).ToList();
                var orderDetails = _mapper.Map<List<OrderDetail>>(orderDetailsDto);
                await _orderService.SaveOrderDetail(orderDetails);

                //clearing cart 
                _cartItemService.ClearCart();

                return Json(new { isValid = true, html = "" });

            }
            return Json(new { isValid = true, html = "" });
        }


    }
}
