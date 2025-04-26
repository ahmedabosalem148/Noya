using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Interfaces;
using Noya.BLL.Services;
using Noya.Models;
using System.Collections.Generic;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            _userService.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.UserId) return BadRequest("ID mismatch.");
            _userService.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
