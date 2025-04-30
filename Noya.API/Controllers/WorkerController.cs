using Microsoft.AspNetCore.Mvc;
using Noya.BLL;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService = new WorkerService();

        [HttpGet]
        public ActionResult<IEnumerable<Worker>> GetAll() => Ok(_workerService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Worker> GetById(int id)
        {
            var worker = _workerService.GetById(id);
            return worker == null ? NotFound() : Ok(worker);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Worker worker)
        {
            _workerService.Create(worker);
            return CreatedAtAction(nameof(GetById), new { id = worker.WorkerId }, worker);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Worker worker)
        {
            if (id != worker.WorkerId) return BadRequest();
            _workerService.Update(worker);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workerService.Delete(id);
            return NoContent();
        }
    }
}
