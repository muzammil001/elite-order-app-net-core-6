using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        
        public int GetOrderBalance(int orderId)
        {
            var totalPaid = _context.PaymentHistories.Where(x => x.OrderId == orderId).Sum(x => x.PaidAmount);
            var orderInDb = _context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (orderInDb == null) return 0;

            var totalBill = orderInDb.TotalAmount;
            return totalBill - totalPaid;

        }
        public int GetAdvanceAmount(int orderId)
        {
            var totalPaid = _context.PaymentHistories.Where(x => x.OrderId == orderId).Sum(x => x.PaidAmount);
            return totalPaid;

        }
        public async Task<List<PaymentHistory>> GetOrderPaymentHistory(int orderId)
        {
            var paymentHistory = await _context.PaymentHistories
                .Include(x => x.Order)
                .Include(x => x.Order.Customer)
                .Where(x => x.OrderId == orderId).ToListAsync();

            return paymentHistory;
        }
        public async Task AddPayment(PaymentHistory payment)
        {
            await _context.PaymentHistories.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public void UpdatePayment(PaymentHistory payment)
        {
            _context.PaymentHistories.Update(payment);
            _context.SaveChanges();
        }

        public async Task<PaymentHistory> GetPaymentTracking(int id)
        {
            var payment = await _context.PaymentHistories.FirstOrDefaultAsync(x => x.Id == id);
            return payment;
        }
       
        public async Task DeletePaymentItem(int id)
        {
            var payment = await _context.PaymentHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (payment != null) _context.PaymentHistories.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}
