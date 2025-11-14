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
        UsersService usersServicies = new UsersService();
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        //List<users> user = new List<users>();
        
        [HttpPost ("")]
        public ActionResult<Users> Post([FromBody] Users value)
        {
            Users user = usersServicies.AddNewUser(value);
            if (user == null)
                return BadRequest("Password is too weak");
            return CreatedAtAction(nameof(Get), new { user.id }, user);
        }

        [HttpPost ("login")]
        public ActionResult<Users> login([FromBody] UpdateUser value)
        {
            Users user = usersServicies.Login(value);
            if(user != null)
            {
                return CreatedAtAction(nameof(Get), new { id = user.id }, user);
            }
            else
                return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] Users userToUpdate)
        {
            usersServicies.UpdateUser(id, userToUpdate);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
