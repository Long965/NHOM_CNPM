using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface IFlowerService
    {
        Task<Flower> GetFlowerByIdAsync(int id);
        Task<IEnumerable<Flower>> GetAllFlowersAsync();
        Task AddFlowerAsync(Flower flower);
        Task UpdateFlowerAsync(Flower flower);
        Task DeleteFlowerAsync(int id);
        //tìm theo số lượng
        Task<List<Flower>> GetFlowersByQuantityAsync(int quantity);
        //tìm theo giá xèn
        Task<List<Flower>> GetFlowersByPriceAsync(decimal price);
        //mua hoa

    }

}
