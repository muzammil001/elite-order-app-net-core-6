using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task NewCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
       await _context.SaveChangesAsync();
    }

    public async Task<Customer> GetCustomer(int key)
    {
        var customerInDb = await _context.Customers.FirstOrDefaultAsync(x => x.Id == key);
        return customerInDb;
    }

    public async Task<bool> CheckCustomer(string mobileNumber)
    {
      return await  _context.Customers.AnyAsync(x=>x.Contact==mobileNumber);
    }

    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (customer != null)
        {
            _context.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}