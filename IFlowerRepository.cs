using ShopFlower.Reponsitories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopFlower.Repositories.Interfaces
{
    public interface IFlowerRepository
    {
        Task<List<Flower>> GetAllFlowersAsync();
        Task<Flower?> GetFlowerByIdAsync(int id);
        Task AddFlowerAsync(Flower flower);
        Task UpdateFlowerAsync(Flower flower);
        Task DeleteFlowerAsync(int id);
    }
}
