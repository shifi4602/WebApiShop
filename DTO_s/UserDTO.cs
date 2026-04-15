using System.ComponentModel.DataAnnotations;

namespace DTO_s
{
    public record UserDTO
    (
        int id,
        string FirstName,
        [StringLength(16, ErrorMessage = "Last name cant be more than 16 letters.")]
        string LastName,
        [EmailAddress (ErrorMessage = "the input must be in email format ... @ . "), Required(ErrorMessage = "email is required")]
        string Email
    );
}
