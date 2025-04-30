using Microsoft.AspNetCore.Mvc;
using Noya.BLL;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerJobController : ControllerBase
    {
        private readonly WorkerJobService _workerJobService = new WorkerJobService();

        [HttpGet("worker/{workerId}")]
        public ActionResult<IEnumerable<WorkerJob>> GetByWorkerId(int workerId)
        {
            return Ok(_workerJobService.GetByWorkerId(workerId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] WorkerJob job)
        {
            _workerJobService.Create(job);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WorkerJob job)
        {
            if (id != job.JobId) return BadRequest();
            _workerJobService.Update(job);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workerJobService.Delete(id);
            return NoContent();
        }
    }
}
