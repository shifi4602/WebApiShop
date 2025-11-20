using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService = new UsersService();
        
        

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok("value");
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users user)
        {
            Users result = _usersService.AddNewUser(user);
            if (result == null)
                return BadRequest("Password is not strong enough");
            return CreatedAtAction(nameof(Get), new { result.Id }, result);
        }

        [HttpPost("login")]
        public ActionResult<Users> Login([FromBody] UpdateUser loginUser)
        {
            Users user = _usersService.Login(loginUser);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] Users userToUpdate)
        {
            _usersService.UpdateUser(id, userToUpdate);
            return NoContent();
        }
    }
}
