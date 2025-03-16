using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedInterfaces.Service;
using DataAccess.Repository;
using Models.Models;

namespace BusinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || !user.PasswordHash.Equals(password))
            {
                return null;
            }
            return user;
        }

        public List<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }
    }
}
