using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface IUserService
    {
        public User Authenticate(string username, string password);
        public List<User> GetAllUser();
        List<User> GetAllAdmin();
    }
}
