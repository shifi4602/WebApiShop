using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsControler : ControllerBase
    {
        passwordServices service = new passwordServices();
        // GET: api/<PasswordControler>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PasswordControler>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public void Get(string pass)
        {
            
        }

        // POST api/<PasswordControler>
        [HttpPost("{pass}")]
        public ActionResult<PassEntity> CheckPasswordStrength([FromBody] string pass)
        {
            PassEntity password = service.GetStrength(pass);
            if (password == null)
                return NoContent();
            return Ok(password);
        }

        // PUT api/<PasswordControler>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PasswordControler>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
