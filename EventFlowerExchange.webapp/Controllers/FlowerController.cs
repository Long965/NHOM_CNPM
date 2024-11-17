using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;
        private readonly IOrderService _orderService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlower(int id)
        {
            var flower = await _flowerService.GetFlowerByIdAsync(id);
            if (flower == null) return NotFound();
            return Ok(flower);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            return Ok(flowers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlower([FromBody] Flower flower)
        {
            await _flowerService.AddFlowerAsync(flower);
            return CreatedAtAction(nameof(GetFlower), new { id = flower.Id }, flower);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlower(int id, [FromBody] Flower updatedFlower)
        {
            if (updatedFlower == null)
            {
                return BadRequest(new { message = "Flower data is missing." });
            }

            if (id != updatedFlower.Id)
            {
                return BadRequest(new { message = "ID in URL does not match ID in the request body." });
            }

            try
            {
                await _flowerService.UpdateFlowerAsync(updatedFlower);
                return Ok(new { message = "Flower updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the flower.", error = ex.Message });
            }
        }



        // API to delete flower by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            try
            {
                await _flowerService.DeleteFlowerAsync(id);
                return Ok(new { message = "Flower deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the flower.", error = ex.Message });
            }
        }

        [HttpGet("quantity/{quantity}")]
        public async Task<IActionResult> GetFlowersByQuantity(int quantity)
        {
            try
            {
                // Gọi dịch vụ để lấy hoa theo số lượng
                var flowers = await _flowerService.GetFlowersByQuantity(quantity);

                // Kiểm tra nếu không có hoa nào phù hợp
                if (flowers == null || flowers.Quantity == 0)
                {
                    return NotFound(new { message = $"No flowers found with quantity {quantity}." });
                }

                return Ok(flowers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving flowers.", error = ex.Message });
            }
        }

        [HttpPost("buy/{id}")]
        public async Task<IActionResult> BuyFlower(int id, [FromQuery] int quantity)
        {
            try
            {
                var flower = await _flowerService.GetFlowerByIdAsync(id);
                if (flower == null)
                {
                    return NotFound(new { message = "Flower not found" });
                }

                // Kiểm tra số lượng có đủ để mua không
                if (flower.Quantity < quantity)
                {
                    return BadRequest(new { message = "Insufficient stock for the requested quantity." });
                }

                // Tính tổng giá tiền
                decimal totalPrice = flower.PricePerUnit.Value * quantity;

                // Giảm số lượng hoa trong kho
                flower.Quantity -= quantity;
                await _flowerService.UpdateFlowerAsync(flower);
                // Lấy thông tin BuyerId từ token hoặc session (giả sử lấy từ claims)
                int buyerId = 1; // Cần lấy ID thực tế từ JWT hoặc session
                int sellerId = flower.SellerId ?? 1;
                // Tạo đơn hàng
                var order = new Order
                {
                    FlowerId = flower.Id,
                    Quantity = quantity,
                    TotalPrice = totalPrice,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    BuyerId = buyerId, // BuyerId của người mua
                    SellerId = sellerId, // SellerId của người bán
                    Status = "Pending"
                };

                // Lưu đơn hàng
                await _orderService.AddOrderAsync(order);

                return Ok(new { message = "Flower purchased successfully.", totalPrice = totalPrice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while purchasing the flower.", error = ex.Message });
            }
        }
    }
}
