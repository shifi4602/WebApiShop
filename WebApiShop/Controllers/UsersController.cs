using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>

        // GET: api/<UsersController>
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
        string filePath = "M:\\WebApi\\WebApiShop\\WebApiShop\\users.txt";
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users value)
        {
            int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
            value.id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(value);
            System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
            return CreatedAtAction(nameof(Get), new { id = value.id }, value);
        }

        [HttpPost ("login")]
        public ActionResult<Users> login([FromBody] UpdateUser value)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    Users user = JsonSerializer.Deserialize<Users>(currentUserInFile);
                    if (user.Email == value.Email && user.Password == value.Password)
                        return CreatedAtAction(nameof(Get), new { id = user.id }, user);
                }
            }
            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Users userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    Users user = JsonSerializer.Deserialize<Users>(currentUserInFile);
                    if (user.id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(filePath, text);
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
