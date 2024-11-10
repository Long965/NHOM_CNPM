using Microsoft.EntityFrameworkCore;
using ShopFlower.Reponsitories.Entities;
using ShopFlower.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopFlower.Repositories.Repository
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly FlowerShopEventContext _context;

        public FlowerRepository(FlowerShopEventContext context)
        {
            _context = context;
        }

        public async Task<List<Flower>> GetAllFlowersAsync()
        {
            return await _context.Flowers.ToListAsync();
        }

        public async Task<Flower?> GetFlowerByIdAsync(int id)
        {
            return await _context.Flowers.FindAsync(id);
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            _context.Flowers.Add(flower);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlowerAsync(Flower flower)
        {
            _context.Flowers.Update(flower);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlowerAsync(int id)
        {
            var flower = await _context.Flowers.FindAsync(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
            }
        }
    }
}
