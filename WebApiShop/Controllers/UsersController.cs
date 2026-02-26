using Microsoft.AspNetCore.Mvc;
using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IpasswordServices _passwordService;

        public UsersController(IUsersService usersService, IpasswordServices passwordService)
        {
            _usersService = usersService;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User value)
        {
            User user = await _usersService.AddNewUser(value);
            if (user == null)
                return BadRequest("Password is too weak");
            return CreatedAtAction(nameof(Get), new { user.Id }, user);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] UpdateUser value)
        {
            User user = await _usersService.Login(value);
            if (user != null)
            {
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            return Unauthorized();
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User userToUpdate)
        {
            bool passwordStrength = await _usersService.UpdateUser(id, userToUpdate);
            if (passwordStrength)
            {
                return Ok(userToUpdate);
            }
            return NoContent();
        }
    }
}
}
