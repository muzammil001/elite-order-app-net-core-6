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
        private readonly PaymentService _paymentService;
        private readonly ReportService _reportService;
        private readonly IMapper _mapper;

        public OrderController(CartService cartItemService, CustomerService customerService, IMapper mapper, OrderService orderService, ItemService itemService, PaymentService paymentService, ReportService reportService)
        {
            _cartItemService = cartItemService;
            _customerService = customerService;
            _mapper = mapper;
            _orderService = orderService;
            _itemService = itemService;
            _paymentService = paymentService;
            _reportService = reportService;
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
                Customers = GetCustomerList(customers)
            };
            return View("NewOrder", order);
        }

        private static List<CustomerDropDownListModel> GetCustomerList(List<Customer> customers)
        {
            if (customers == null) throw new ArgumentNullException(nameof(customers));
            return customers.Select(x => new CustomerDropDownListModel()
            {
                Id = x.Id,
                Name = $"{x.Name} - {x.Contact}"
            }).ToList();
        }

        public IActionResult ManageOrders()
        {
            return View();
        }
        public IActionResult CompletedOrders()
        {
            return View();
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            //clearing cart 
            _cartItemService.ClearCart();

            var items = await _itemService.GetAll();
            var customers = await _customerService.GetAll();
            var orderInDb = await _orderService.GetOrder(id);

            var order = new OrderModel()
            {
                Order = _mapper.Map<OrderDto>(orderInDb),
                Items = items.ToList(),
                Customers = GetCustomerList(customers)
            };
            return View("EditOrder", order);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(OrderModel model, int id = 0)
        {
            if (!TryValidateModel(model.Order))
                return BadRequest(ModelState.GetFullErrorMessage());

            if (model.Order.TotalAmount == "0")
            {
                return BadRequest("Please enter order total amount.");
            }

            if (model.Order.Id == 0)
            {
                var cartItems = await _cartItemService.GetAll();
                if (cartItems.Count == 0)
                {
                    return BadRequest("Please add items in cart.");
                }

                //Saving order
                var order = new Order()
                {
                    OrderDate = model.Order.OrderDate,
                    DeliveryDate = model.Order.DeliveryDate,
                    CustomerId = model.Order.CustomerId ?? 0,
                    TotalAmount = Convert.ToInt32(model.Order.TotalAmount.Replace(",", "")),
                    AdvancePayment = Convert.ToInt32(model.Order.AdvancePayment.Replace(",", "")),
                    IsPending = true,
                    IsCompleted = false,
                };
                var orderId = await _orderService.SaveOrder(order);

                //Saving order details
                var orderDetailsDto = _cartItemService.GetAll().Result.Select(x => new OrderDetailDto()
                {
                    ItemId = x.ItemId,
                    OrderId = orderId
                }).ToList();
                var orderDetails = _mapper.Map<List<OrderDetail>>(orderDetailsDto);
                await _orderService.SaveOrderDetail(orderDetails);

                var payment = new PaymentHistory()
                {
                    OrderId = orderId,
                    PaidDate = DateTime.Today,
                    PaidAmount = Convert.ToInt32(model.Order.AdvancePayment.Replace(",", "")),
                    Description = "Advance Payment"
                };
                await _paymentService.AddPayment(payment);

                //clearing cart 
                _cartItemService.ClearCart();

                return Json(new { isValid = true, html = "" });

            }
            else
            {
                var orderInDb = await _orderService.GetOrder(id);


                orderInDb.OrderDate = model.Order.OrderDate;
                orderInDb.DeliveryDate = model.Order.DeliveryDate;
                orderInDb.CustomerId = model.Order.CustomerId ?? 0;
                orderInDb.TotalAmount = Convert.ToInt32(model.Order.TotalAmount.Replace(",", ""));
                orderInDb.AdvancePayment = Convert.ToInt32(model.Order.AdvancePayment.Replace(",", ""));


                _orderService.UpdateOrder(orderInDb);

                //clearing cart 
                _cartItemService.ClearCart();

                return Json(new { isValid = true, html = "" });
            }
        }

        public async Task<ActionResult> CustomerDropDown()
        {
            var customers = await _customerService.GetAll();

            var order = new OrderModel()
            {
                Customers = GetCustomerList(customers)
            };
            return PartialView("_CustomerDropDownList",order);
        }

        public async Task<ActionResult> ItemDropDown()
        {
            var items = await _itemService.GetAll();

            var order = new OrderModel()
            {
                Items = items.ToList()
            };
            return PartialView("_ItemDropDownList", order);
        }

        public async Task<IActionResult> OrderInvoice(int id)
        {
            var invoice = await _reportService.GetOrderInvoice(id);
            var model = new ReportModel()
            {
                Invoice = invoice,
                IsInvoice = true,
                PageTitle = "Invoice"
            };
            return View("Report",model);
        }
        public IActionResult PaymentHistory(int orderId)
        {
            return View();
        }
        public async Task<IActionResult> PaymentHistoryReport(int id)
        {
            var history = await _reportService.GetOrderPaymentHistoryReport(id);
            var model = new ReportModel()
            {
                PaymentHistory = history,
                IsInvoice = false,
                PageTitle = "Payment History"


            };
            return View("Report", model);
        }
    }
}
