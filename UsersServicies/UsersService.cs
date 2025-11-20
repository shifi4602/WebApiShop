
using Enteties;
using Repositories;

namespace Services
{
    public class UsersService : IUsersService
    {
        IUsersRepository _iUsersRepository;
        IpasswordServices _iPasswordServices;
        public UsersService(IUsersRepository usersRepository, IpasswordServices passwordServices) 
        {
            _iUsersRepository = usersRepository;
            _iPasswordServices = passwordServices;
        }
        public Users AddNewUser(Users user)
        {
            if (_iPasswordServices.GetStrength(user.Password).Strength <= 2)
                return null;
            return _iUsersRepository.AddUser(user);
        }

        public Users Login(UpdateUser user)
        {
            return _iUsersRepository.login(user);
        }

        public bool UpdateUser(int id, Users userToUpdate)
        {
            if (_iPasswordServices.GetStrength(userToUpdate.Password).Strength <= 2)
            {
                return false;
            }
            _iUsersRepository.UpdateUser(id, userToUpdate);
            return true;

        }
    }
}
