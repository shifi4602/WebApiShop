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
        private readonly IUsersService _usersService;
        private readonly IPasswordServices _passwordServices;
        
        public UsersController(IUsersService usersService, IPasswordServices passwordServices)
        {
            _passwordServices = passwordServices;
            _usersService = usersService;
        }
        
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok("value");
        }
        
        // POST api/<UsersController>
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
                return Ok(user);
            }
            return Unauthorized();
        }
        
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User userToUpdate)
        {
            bool passwordsStrenght = await _usersService.UpdateUser(id, userToUpdate);
            if (passwordsStrenght)
            {
                return NoContent();
            }
            return BadRequest("Password is too weak");
        
        }
    }
}
