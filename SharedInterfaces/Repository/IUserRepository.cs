using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace SharedInterfaces.Repository
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
    }
}
