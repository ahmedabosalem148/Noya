using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("with-products")]
        public ActionResult<IEnumerable<Category>> GetAllWithProducts()
        {
            var categories = _categoryService.GetAllWithProducts();
            return Ok(categories);
        }


        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Category category)
        {
            _categoryService.Create(category);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId) return BadRequest("ID mismatch.");
            _categoryService.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return NoContent();
        }
    }
}
