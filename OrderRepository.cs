using Microsoft.EntityFrameworkCore;
using ShopFlower.Reponsitories.Entities;
using ShopFlower.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopFlower.Repositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FlowerShopEventContext _context;

        public OrderRepository(FlowerShopEventContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderDetails)
                                        .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = "Confirmed";
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
