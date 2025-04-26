using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController()
        {
            _providerService = new ProviderService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetAll()
        {
            var providers = _providerService.GetAll();
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public ActionResult<Provider> GetById(int id)
        {
            var provider = _providerService.GetById(id);
            if (provider == null) return NotFound();
            return Ok(provider);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Provider provider)
        {
            _providerService.Create(provider);
            return CreatedAtAction(nameof(GetById), new { id = provider.ProviderId }, provider);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Provider provider)
        {
            if (id != provider.ProviderId) return BadRequest("ID mismatch.");
            _providerService.Update(provider);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _providerService.Delete(id);
            return NoContent();
        }
    }
}
