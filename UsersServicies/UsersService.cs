using AutoMapper;
using DTO_s;
using Enteties;
using Repositories;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IpasswordServices _passwordServices;
        IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IpasswordServices passwordServices, IMapper imapper)
        {
            _userRepository = usersRepository;
            _passwordServices = passwordServices;
            _mapper = imapper;
        }
        public async Task<UserDTO> AddNewUser(postUserDto newUser)
        {
            User user = _mapper.Map<postUserDto, User>(newUser);
            User userResult = await _userRepository.AddUser(user);
            UserDTO userDTOres = _mapper.Map<User, UserDTO>(userResult);
            if (_passwordServices.GetStrength(user.Password).Strength <= 2)
                return null;
            return userDTOres;
        }

        public async Task<User> Login(ExisitingUser user)
        {
            return await _userRepository.login(user.Email, user.Password);
        }

        public async Task<bool> UpdateUser(int id, postUserDto userToUpdate)
        {
            if (_passwordServices.GetStrength(userToUpdate.Password).Strength <= 2)
            {
                return false;
            }
            User user = _mapper.Map<postUserDto, User>(userToUpdate);
            await _userRepository.UpdateUser(user);
            return true;
        }

        public async Task<UserDTO> GetById(int id)
        {
            User user = await _userRepository.GetById(id);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> UserWithSameEmail(string email, int id = -1)
        {
            return await _userRepository.UserWithSameEmail(email, id);
        }
    }
}
