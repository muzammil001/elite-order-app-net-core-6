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
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllPendingOrders()
        {
            return await _context.Orders.Include(x => x.Customer).Where(x => x.IsPending).OrderByDescending(x => x.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetAllCompletedOrders()
        {
            return await _context.Orders.Include(x => x.Customer).Where(x => x.IsCompleted).OrderByDescending(x => x.OrderDate).ToListAsync();
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


        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public async Task CompleteOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                order.IsCompleted = true;
                order.IsPending = false;
            }

            await _context.SaveChangesAsync();
        }

        public async Task PendingOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                order.IsCompleted = false;
                order.IsPending = true;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        #region Order Details
        public async Task AddOrderItem(OrderDetail item)
        {
            var orderDetail = new OrderDetail()
            {
                ItemId = item.ItemId,
                OrderId = item.OrderId
            };
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var orderDetail = await _context.OrderDetails
                .Include(x => x.Order)
                .Include(x => x.Item)
                .Include(x => x.Order.Customer)
                .Where(x => x.OrderId == orderId).ToListAsync();

            return orderDetail;
        }

        public async Task SaveOrderDetail(List<OrderDetail> orderDetails)
        {
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailItem(int id)
        {
            var order = await _context.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsItemExists(int itemId, int orderId)
        {
            return await _context.OrderDetails.AnyAsync(x => x.ItemId == itemId && x.OrderId == orderId);
        }
        #endregion

    }
}
