using System.ComponentModel.DataAnnotations;

namespace DTO_s
{
    public record UserDTO
    (
        int id,
        string FirstName,
        string LastName,
        string Email,
        ICollection<OrdersDTO> Orders
    );
}
