using FlowerShop.Models;
using System.Collections.Generic;

namespace FlowerShop.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void ConfirmOrder(int id);
    }
}
