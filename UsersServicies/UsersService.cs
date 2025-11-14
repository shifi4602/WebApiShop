
using Enteties;
using Repositories;

namespace Services
{
    public class UsersService
    {
        UsersRepository usersRepository = new UsersRepository();
        passwordServices passwordServices = new passwordServices();
        public Users AddNewUser(Users user)
        {
            if (passwordServices.GetStrength(user.Password).Strength <= 2)
                return null;
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
