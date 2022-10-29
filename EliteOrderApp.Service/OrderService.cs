using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteOrderApp.Service
{

    public class OrderService
    {
        private AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllPendingOrders()
        {
            return await _context.Orders.Include(x=>x.Customer).Where(x => x.IsPending).OrderByDescending(x => x.OrderDate).ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<int> SaveOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var orderDetail = await _context.OrderDetails
                .Include(x => x.Item)
                .Where(x => x.OrderId == orderId).ToListAsync();
            
            return orderDetail;
        }

        public async Task SaveOrderDetail(List<OrderDetail> orderDetails)
        {
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }
    }
}
