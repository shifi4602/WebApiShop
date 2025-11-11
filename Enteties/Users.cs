using System.ComponentModel.DataAnnotations;

namespace Enteties
{
    public class Users
    {
        public int id { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [StringLength(8, ErrorMessage = "password can be between 4 to 8 chars", MinimumLength = 4), Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
