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
        public User GetUserById(int userId)
        {
            try
            {
                return _context.Users.Include(u => u.Employee).Include(u => u.Employee.Department).AsNoTracking().FirstOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
        }

    }
}
