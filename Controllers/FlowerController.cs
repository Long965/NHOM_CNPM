using FlowerShop.Models;
using FlowerShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlowerShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Flower>> GetAllFlowers()
        {
            return Ok(_flowerService.GetAllFlowers());
        }

        [HttpGet("{id}")]
        public ActionResult<Flower> GetFlowerById(int id)
        {
            var flower = _flowerService.GetFlowerById(id);
            if (flower == null)
                return NotFound();
            return Ok(flower);
        }

        [HttpPost]
        public IActionResult AddFlower([FromBody] Flower flower)
        {
            _flowerService.AddFlower(flower);
            return CreatedAtAction(nameof(GetFlowerById), new { id = flower.Id }, flower);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFlower(int id, [FromBody] Flower flower)
        {
            if (id != flower.Id)
                return BadRequest();

            _flowerService.UpdateFlower(flower);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlower(int id)
        {
            _flowerService.DeleteFlower(id);
            return NoContent();
        }
    }
}
