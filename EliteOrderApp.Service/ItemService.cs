using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class ItemService
{
    private AppDbContext _context;

    public ItemService(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Item> GetAll()
    {
        return _context.Items.ToList();
    }

    public void NewItem(Item item)
    {
        _context.Items.Add(item);
        _context.SaveChanges();
    }
    public async Task<Item?> GetItem(int key)
    {
        var itemInDb = await _context.Items.FirstOrDefaultAsync(x => x.Id == key);
        return itemInDb;
    }

    public void UpdateItem()
    {
        _context.SaveChanges();
    }

    public void DeleteItem(int id)
    {
        var item = _context.Items.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}