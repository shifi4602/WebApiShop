using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly passwordServices _passwordService = new passwordServices();

        // POST api/<PasswordsController>
        [HttpPost("{pass}")]
        public ActionResult<PassEntity> CheckPasswordStrength([FromBody] string pass)
        {
            PassEntity password = _passwordService.GetStrength(pass);
            if (password == null)
                return BadRequest("Invalid password");
            return Ok(password);
        }
    }
}
