using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingAgencyController : ControllerBase
    {
        private readonly IShippingAgencyService _shippingAgencyService;

        public ShippingAgencyController()
        {
            _shippingAgencyService = new ShippingAgencyService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShippingAgency>> GetAll()
        {
            var agencies = _shippingAgencyService.GetAll();
            return Ok(agencies);
        }

        [HttpGet("{id}")]
        public ActionResult<ShippingAgency> GetById(int id)
        {
            var agency = _shippingAgencyService.GetById(id);
            if (agency == null) return NotFound();
            return Ok(agency);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ShippingAgency agency)
        {
            _shippingAgencyService.Create(agency);
            return CreatedAtAction(nameof(GetById), new { id = agency.AgencyId }, agency);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ShippingAgency agency)
        {
            if (id != agency.AgencyId) return BadRequest("ID mismatch.");
            _shippingAgencyService.Update(agency);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _shippingAgencyService.Delete(id);
            return NoContent();
        }
    }
}
