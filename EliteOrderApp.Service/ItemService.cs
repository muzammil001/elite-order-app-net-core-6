using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EliteOrderApp.Service;

public class ItemService
{
    private readonly AppDbContext _context;

    public ItemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Item>> GetAll()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task NewItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task<Item?> GetItem(int key)
    {
        var itemInDb = await _context.Items.FirstOrDefaultAsync(x => x.Id == key);
        return itemInDb;
    }

    public void UpdateItem(Item item)
    {
        _context.Items.Update(item);
        _context.SaveChanges();
    }

    public async Task DeleteItem(int id)
    {
        var item = _context.Items.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}