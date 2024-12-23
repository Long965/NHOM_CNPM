﻿using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerService(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository ?? throw new ArgumentNullException(nameof(flowerRepository));
        }

        public async Task<Flower> GetFlowerByIdAsync(int id)
        {
            try
            {
                return await _flowerRepository.GetFlowerByIdAsync(id);
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new KeyNotFoundException(knfEx.Message, knfEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while retrieving flower by ID", ex);
            }
        }

        public async Task<IEnumerable<Flower>> GetAllFlowersAsync()
        {
            try
            {
                return await _flowerRepository.GetAllFlowersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while retrieving all flowers", ex);
            }
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            if (flower == null) throw new ArgumentNullException(nameof(flower));

            try
            {
                await _flowerRepository.AddFlowerAsync(flower);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while adding flower", ex);
            }
        }

        public async Task UpdateFlowerAsync(Flower updatedFlower)
        {
            if (updatedFlower == null)
                throw new ArgumentNullException(nameof(updatedFlower));

            try
            {
                var existingFlower = await _flowerRepository.GetFlowerByIdAsync(updatedFlower.Id);
                if (existingFlower == null)
                    throw new Exception("Flower not found");

                existingFlower.Name = updatedFlower.Name;
                existingFlower.Quantity = updatedFlower.Quantity;
                existingFlower.Condition = updatedFlower.Condition;
                existingFlower.PricePerUnit = updatedFlower.PricePerUnit;
                existingFlower.SellerId = updatedFlower.SellerId;
                existingFlower.Description = updatedFlower.Description;

                await _flowerRepository.UpdateFlowerAsync(existingFlower);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while updating flower", ex);
            }
        }


        public async Task DeleteFlowerAsync(int flowerId)
        {
            try
            {
                var existingFlower = await _flowerRepository.GetFlowerByIdAsync(flowerId);
                if (existingFlower == null)
                    throw new Exception("Flower not found");

                await _flowerRepository.DeleteFlowerAsync(existingFlower);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while deleting flower", ex);
            }
        }

        // tìm theo số lượng
        public async Task<List<Flower>> GetFlowersByQuantityAsync(int quantity)
        {
            // Gọi repository để lấy danh sách hoa có số lượng tương ứng
            return await _flowerRepository.GetFlowersByQuantityAsync(quantity);
        }
        //tìm theo giá xèn
        public async Task<List<Flower>> GetFlowersByPriceAsync(decimal price)
        {
            // Gọi repository để lấy danh sách hoa có giá tiền tương ứng
            return await _flowerRepository.GetFlowersByPriceAsync(price);
        }


        //cập nhật số lượng hoa sau khi mua

       

    }
}
