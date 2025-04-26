using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController()
        {
            _inventoryService = new InventoryService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Inventory>> GetAll()
        {
            var inventory = _inventoryService.GetAll();
            return Ok(inventory);
        }

        [HttpGet("{id}")]
        public ActionResult<Inventory> GetById(int id)
        {
            var inventoryItem = _inventoryService.GetById(id);
            if (inventoryItem == null) return NotFound();
            return Ok(inventoryItem);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Inventory inventory)
        {
            _inventoryService.Create(inventory);
            return CreatedAtAction(nameof(GetById), new { id = inventory.InventoryId }, inventory);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.InventoryId) return BadRequest("ID mismatch.");
            _inventoryService.Update(inventory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _inventoryService.Delete(id);
            return NoContent();
        }
    }
}
