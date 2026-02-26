namespace DTO_s
{
    public record UserDTO
    (
        int Id,
        string FirstName,
        string LastName,
        [EmailAddress]
        string Email
    );
}
