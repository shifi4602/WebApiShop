
using Enteties;
using UsersRepositories;

namespace UsersServicies
{
    public class UsersService
    {
        UsersRepository usersRepository = new UsersRepository();
        public Users AddNewUser(Users user)
        {
            return usersRepository.AddUser(user);
        }

        public Users Login(UpdateUser user)
        {
            return usersRepository.login(user);
        }

        public void UpdateUser(int id, Users userToUpdate)
        {
            usersRepository.UpdateUser(id, userToUpdate);
        }
    }
}
