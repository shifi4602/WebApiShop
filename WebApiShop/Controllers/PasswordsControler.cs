using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Services;

namespace Enteties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsControler : ControllerBase, IPasswordsControler
    {
        private readonly IpasswordServices _iPasswordService;
        public PasswordsControler(IpasswordServices iPasswordControler)
        {
            _iPasswordService = iPasswordControler;
        }

        // POST api/<PasswordControler>
        [HttpPost("CheckPasswordStrength")]
        public ActionResult<PassEntity> CheckPasswordStrength([FromBody] string pass)
        {
            PassEntity password = _iPasswordService.GetStrength(pass);
            if (password == null)
                return NoContent();
            return Ok(password);
        }
    }
}
