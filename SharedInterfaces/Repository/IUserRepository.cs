using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
        public void AddUser(User user); 
    }
}
