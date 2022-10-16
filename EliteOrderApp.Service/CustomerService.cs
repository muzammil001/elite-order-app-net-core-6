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

    public ICollection<Customer> GetAll()
    {
        return _context.Customers.ToList();
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

    public void UpdateCustomer()
    {
        _context.SaveChanges();
    }

    public void DeleteCustomer(int id)
    {
        var Customer = _context.Customers.FirstOrDefault(x => x.Id == id);
        if (Customer != null)
        {
            _context.Remove(Customer);
            _context.SaveChanges();
        }
    }
}