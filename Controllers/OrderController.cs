using FlowerShop.Models;
using FlowerShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlowerShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            _orderService.CreateOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest();

            _orderService.UpdateOrder(order);
            return NoContent();
        }

        [HttpPut("{id}/confirm")]
        public IActionResult ConfirmOrder(int id)
        {
            _orderService.ConfirmOrder(id);
            return NoContent();
        }
    }
}
