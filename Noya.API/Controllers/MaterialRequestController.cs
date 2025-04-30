using Microsoft.AspNetCore.Mvc;
using Noya.BLL;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialRequestController : ControllerBase
    {
        private readonly MaterialRequestService _materialRequestService = new MaterialRequestService();

        [HttpGet]
        public ActionResult<IEnumerable<MaterialRequest>> GetAll() => Ok(_materialRequestService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<MaterialRequest> GetById(int id)
        {
            var req = _materialRequestService.GetById(id);
            return req == null ? NotFound() : Ok(req);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MaterialRequest req)
        {
            _materialRequestService.Create(req);
            return CreatedAtAction(nameof(GetById), new { id = req.RequestId }, req);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MaterialRequest req)
        {
            if (id != req.RequestId) return BadRequest();
            _materialRequestService.Update(req);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _materialRequestService.Delete(id);
            return NoContent();
        }
    }
}
