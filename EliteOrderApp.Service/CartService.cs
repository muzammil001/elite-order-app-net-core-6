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
        public ICollection<Cart> GetAll()
        {
            return _context.CartItems.Include(x=>x.Item).ToList();
        }
        public async Task<Cart?> GetItem(int key)
        {
            var itemInDb = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == key);
            return itemInDb;
        }
        public void AddItemInCart(Item item)
        {
            var cartItem = new Cart()
            {
                ItemId = item.Id,
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCart()
        {
            _context.SaveChanges();
        }

        public void DeleteCartItem(int id)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
