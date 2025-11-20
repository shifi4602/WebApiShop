
using Enteties;
using Repositories;

namespace Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository = new UsersRepository();
        private readonly passwordServices _passwordServices = new passwordServices();
        
        public Users AddNewUser(Users user)
        {
            if (_passwordServices.GetStrength(user.Password).Strength < 2)
                return null;
            return _usersRepository.AddUser(user);
        }

        public Users Login(UpdateUser user)
        {
            return _usersRepository.Login(user);
        }

        public void UpdateUser(int id, Users userToUpdate)
        {
            _usersRepository.UpdateUser(id, userToUpdate);
        }
    }
}
