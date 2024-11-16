using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface IFlowerRepository
    {
        Task<Flower> GetFlowerByIdAsync(int id);
        Task<IEnumerable<Flower>> GetAllFlowersAsync();
        Task AddFlowerAsync(Flower flower);
        Task UpdateFlowerAsync(Flower flower);
        Task DeleteFlowerAsync(Flower flower);
        //tìm theo sl
        Task<List<Flower>> GetFlowersByQuantityAsync(int quantity);
        //tìm theo giá
        Task<List<Flower>> GetFlowersByPriceAsync(decimal price);
        Task UpdateAsync(Flower flower);
        //mua hoa


    }
}
