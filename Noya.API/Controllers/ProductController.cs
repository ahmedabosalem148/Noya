using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product product)
        {
            _productService.Create(product);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Product product)
        {
            if (id != product.ProductId) return BadRequest("ID mismatch.");
            _productService.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _productService.Delete(id);
            return NoContent();
        }
    }
}
