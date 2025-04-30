using Microsoft.AspNetCore.Mvc;
using Noya.BLL;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _materialService = new MaterialService();

        [HttpGet]
        public ActionResult<IEnumerable<Material>> GetAll() => Ok(_materialService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Material> GetById(int id)
        {
            var mat = _materialService.GetById(id);
            return mat == null ? NotFound() : Ok(mat);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Material mat)
        {
            _materialService.Create(mat);
            return CreatedAtAction(nameof(GetById), new { id = mat.MaterialId }, mat);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Material mat)
        {
            if (id != mat.MaterialId) return BadRequest();
            _materialService.Update(mat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _materialService.Delete(id);
            return NoContent();
        }
    }
}
