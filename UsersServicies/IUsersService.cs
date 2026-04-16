using DTO_s;
using Enteties;

namespace Services
{
    public interface IUsersService
    {
        Task<UserDTO> AddNewUser(postUserDto newuser);
        Task<User> Login(ExisitingUser user);
        Task<bool> UpdateUser(int id, postUserDto userToUpdate);
        Task<UserDTO> GetById(int id);
        Task<bool> UserWithSameEmail(string email, int id = -1);
    }
}