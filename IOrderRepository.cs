using ShopFlower.Reponsitories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopFlower.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task ConfirmOrderAsync(int id);
    }
}
