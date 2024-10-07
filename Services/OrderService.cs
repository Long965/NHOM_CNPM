using FlowerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowerShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();

        public IEnumerable<Order> GetAllOrders() => _orders;

        public Order GetOrderById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public void CreateOrder(Order order)
        {
            order.Id = _orders.Count + 1;
            order.OrderDate = DateTime.Now;
            order.Status = "Processing";
            _orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = GetOrderById(order.Id);
            if (existingOrder != null)
            {
                existingOrder.Flowers = order.Flowers;
                existingOrder.Status = order.Status;
            }
        }

        public void ConfirmOrder(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                order.Status = "Confirmed";
            }
        }
    }
}
