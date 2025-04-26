using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingDetailController : ControllerBase
    {
        private readonly IShippingDetailService _shippingDetailService;

        public ShippingDetailController()
        {
            _shippingDetailService = new ShippingDetailService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShippingDetail>> GetAll()
        {
            var details = _shippingDetailService.GetAll();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public ActionResult<ShippingDetail> GetById(int id)
        {
            var detail = _shippingDetailService.GetById(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ShippingDetail detail)
        {
            _shippingDetailService.Create(detail);
            return CreatedAtAction(nameof(GetById), new { id = detail.ShippingId }, detail);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ShippingDetail detail)
        {
            if (id != detail.ShippingId) return BadRequest("ID mismatch.");
            _shippingDetailService.Update(detail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _shippingDetailService.Delete(id);
            return NoContent();
        }
    }
}
