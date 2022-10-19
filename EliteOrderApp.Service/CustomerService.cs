using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class CustomerService
{
    private AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }

    public void NewCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }
    public async Task<Customer?> GetCustomer(int key)
    {
        var CustomerInDb = await _context.Customers.FirstOrDefaultAsync(x => x.Id == key);
        return CustomerInDb;
    }

    public async Task<bool> CheckCustomer(string mobileNumber)
    {
      return await  _context.Customers.AnyAsync(x=>x.Contact==mobileNumber);
    }

    public void UpdateCustomer()
    {
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