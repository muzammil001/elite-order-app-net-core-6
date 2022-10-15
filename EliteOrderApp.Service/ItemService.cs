using EliteOrderApp.Database;
using EliteOrderApp.Domain.Entities;

namespace EliteOrderApp.Service;

public class ItemService
{
    private AppDbContext _context;

    public ItemService(AppDbContext context)
    {
        _context = context;
    }

    public List<Item> GetAll()
    {
        return _context.Items.ToList();
    }
}