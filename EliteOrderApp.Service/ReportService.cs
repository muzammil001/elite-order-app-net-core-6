using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using EliteOrderApp.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service
{
	public class ReportService
	{
        private readonly OrderService _orderService;
        private readonly PaymentService _paymentService;

        public ReportService(OrderService orderService, PaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public async Task<List<InvoiceModel>> GetOrderInvoice(int orderId)
        {
            var saleItems =await _orderService.GetOrderDetails(orderId);

            return saleItems.Select(item => new InvoiceModel()
                {
                    AdvanceAmount = _paymentService.GetReceivedAmount(item.OrderId),
                    CustomerName = item.Order.Customer.Name,
                    DeliveryDate = item.Order.DeliveryDate,
                    OrderDate = item.Order.OrderDate,
                    ItemName = item.Item.Name,
                    TotalBill = item.Order.TotalAmount,
                    OrderNumber = item.OrderId,
                    CustomerContact = item.Order.Customer.Contact,
                })
                .ToList();
        }

        public async Task<List<PaymentHistoryModel>> GetOrderPaymentHistoryReport(int orderId)
        {
            var historyList = new List<PaymentHistoryModel>();

            var paymentDetails = await _paymentService.GetOrderPaymentHistory(orderId);
            foreach (var item in paymentDetails)
            {
                var payment = new PaymentHistoryModel
                {
                    PaidDate = item.PaidDate,
                    PaidAmount = item.PaidAmount,
                    CustomerName = item.Order.Customer.Name,
                    CustomerContactNumber = item.Order.Customer.Contact
                };
                historyList.Add(payment);
            }

            return historyList;
        }
	}
}
