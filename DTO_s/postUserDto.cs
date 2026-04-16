using Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s
{
    public record postUserDto
    (
     int Id,

     [EmailAddress (ErrorMessage = "the input must be in email format ... @ . "), Required(ErrorMessage = "email is required")]
     string Email,

     string FirstName,

     [StringLength(16, ErrorMessage = "Last name cant be more than 16 letters.")]
     string LastName,

     [Required]
     string Password
    );   
}
