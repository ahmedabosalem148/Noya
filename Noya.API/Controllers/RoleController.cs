using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController()
        {
            _roleService = new RoleService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetById(int id)
        {
            var role = _roleService.GetById(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Role role)
        {
            _roleService.Create(role);
            return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, role);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Role role)
        {
            if (id != role.RoleId) return BadRequest("ID mismatch.");
            _roleService.Update(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _roleService.Delete(id);
            return NoContent();
        }
    }
}
