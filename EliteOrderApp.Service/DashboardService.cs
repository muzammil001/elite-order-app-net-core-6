using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalPendingOrdersCount()
    {
        return  await _context.Orders.CountAsync(x => x.IsPending);
    }
    
    public async Task<int> GetUpcomingDeliveriesCount(DateTime date)
    {
        return  await _context.Orders.Where(x => x.DeliveryDate.Month==date.Month).CountAsync();
    }
    
    public async Task<int> GetRevenueByYearSum(DateTime date )
    {
        return await _context.Orders.Where(x => x.OrderDate.Year==date.Year).SumAsync(x => x.TotalAmount);
    }
    
    public async Task<List<RevenueModel>> GetOrderRevenue()
    {
        var orders = await _context.Orders.ToListAsync();
        var data = orders.Select(o => new { o.OrderDate.Year, o.OrderDate.Month, o.TotalAmount })
            .GroupBy(x => new { x.Year }, (key, group) => new  RevenueModel()
        {  
            Year = key.Year,  
            Amount = group.Sum(k => k.TotalAmount)  
        }).ToList();

        return data;
    }


    public async Task<int> GetTotalCashRecoverySum()
    {
        var pendingOrders = await _context.PaymentHistories.Include(x=>x.Order).Where(x => x.Order.IsPending).SumAsync(x=>x.PaidAmount);
        var allTotalOrders = await _context.Orders.Where(x => x.IsPending).SumAsync(x => x.TotalAmount);
        var total = allTotalOrders - pendingOrders;
        return total;
    }
}

public class RevenueModel
{
    public int Year { get; set; }
    public int Amount { get; set; }
}
