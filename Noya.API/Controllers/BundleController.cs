using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BundleController : ControllerBase
    {
        private readonly IBundleService _bundleService;

        public BundleController()
        {
            _bundleService = new BundleService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Bundle>> GetAll()
        {
            var bundles = _bundleService.GetAll();
            return Ok(bundles);
        }

        [HttpGet("{id}")]
        public ActionResult<Bundle> GetById(int id)
        {
            var bundle = _bundleService.GetById(id);
            if (bundle == null) return NotFound();
            return Ok(bundle);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Bundle bundle)
        {
            _bundleService.Create(bundle);
            return CreatedAtAction(nameof(GetById), new { id = bundle.BundleId }, bundle);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Bundle bundle)
        {
            if (id != bundle.BundleId) return BadRequest("ID mismatch.");
            _bundleService.Update(bundle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _bundleService.Delete(id);
            return NoContent();
        }
    }
}
