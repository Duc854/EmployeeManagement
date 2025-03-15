using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;

namespace DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementContext _context;
        public EmployeeRepository()
        {
            _context = new EmployeeManagementContext();
        }
        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.User)
                    .FirstOrDefault(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            try
            {
                var existingEmployee = _context.Employees.Find(employee.EmployeeId);
                if (existingEmployee != null)
                {
                    int originalUserId = existingEmployee.UserId; 

                    _context.Entry(existingEmployee).CurrentValues.SetValues(employee);

                    existingEmployee.UserId = originalUserId;

                    if (employee.Avatar == null)
                    {
                        existingEmployee.Avatar = existingEmployee.Avatar;
                    }

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee != null)
                {
                    // Xóa User 
                    var user = _context.Users.Find(employee.UserId);
                    if (user != null)
                    {
                        _context.Users.Remove(user);
                    }

                    // Xóa Employee
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void SaveEmployeeAvatar(int employeeId, byte[] avatarData)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee != null)
                {
                    employee.Avatar = avatarData;
                    if(employee.Avatar.Length > 5 * 1024 * 1024)
                    {
                        employee.Avatar = null;
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public byte[] GetEmployeeAvatar(int employeeId)
        {
            try
            {
                var employee = _context.Employees
                    .AsNoTracking()
                    .Where(e => e.EmployeeId == employeeId)
                    .Select(e => e.Avatar)
                    .FirstOrDefault();

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public List<Employee> SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return _context.Employees.ToList();
            }
                keyword = keyword.ToLower();
                
                return _context.Employees
                .Include(e => e.Department)
                .Include(e => e.User)
                .Where(e => e.FullName.ToLower().Contains(keyword) ||
                            e.Department.DepartmentName.ToLower().Contains(keyword) ||
                            e.Position.ToLower().Contains(keyword) )
                .ToList();
        }
    }
}
