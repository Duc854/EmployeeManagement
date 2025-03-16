using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedInterfaces;
using Models.Models;
using SharedInterfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeManagementContext _context;

        public UserRepository()
        {
            _context = new EmployeeManagementContext();
        }

        public List<User> GetAllUser()
        {
            try
            {
                return _context.Users
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return _context.Users.Include(u => u.Employee).Include(u => u.Employee.Department).AsNoTracking().FirstOrDefault(u => u.Username.Equals(username));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
