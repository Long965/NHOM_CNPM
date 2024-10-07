using FlowerShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlowerShop.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly List<Flower> _flowers = new();

        public IEnumerable<Flower> GetAllFlowers() => _flowers;

        public Flower GetFlowerById(int id) => _flowers.FirstOrDefault(f => f.Id == id);

        public void AddFlower(Flower flower)
        {
            flower.Id = _flowers.Count + 1;
            _flowers.Add(flower);
        }

        public void UpdateFlower(Flower flower)
        {
            var existingFlower = GetFlowerById(flower.Id);
            if (existingFlower != null)
            {
                existingFlower.Name = flower.Name;
                existingFlower.Type = flower.Type;
                existingFlower.Quantity = flower.Quantity;
                existingFlower.Status = flower.Status;
            }
        }

        public void DeleteFlower(int id) => _flowers.RemoveAll(f => f.Id == id);
    }
}
