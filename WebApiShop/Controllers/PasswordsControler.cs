using Microsoft.AspNetCore.Mvc;
using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly IpasswordServices _passwordService;

        public PasswordsController(IpasswordServices passwordService)
        {
            _passwordService = passwordService;
        }

        // POST api/<PasswordController>
        [HttpPost("CheckPasswordStrength")]
        public ActionResult<PassEntity> CheckPasswordStrength([FromBody] string pass)
        {
            PassEntity password = _passwordService.GetStrength(pass);
            if (password == null)
                return BadRequest("Invalid password");
            return Ok(password);
        }
    }
}
