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
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Cart>> GetAll()
        {
            return await _context.CartItems.Include(x => x.Item).ToListAsync();
        }

        public async Task<Cart?> GetItem(int key)
        {
            var itemInDb = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == key);
            return itemInDb;
        }
        public async Task AddItemInCart(Cart item)
        {
            var cartItem = new Cart()
            {
                ItemId = item.ItemId,
            };
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsItemExists(int itemId)
        {
            return await _context.CartItems.AnyAsync(x => x.ItemId == itemId);
        }

        public async Task UpdateCart()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItem(int id)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
