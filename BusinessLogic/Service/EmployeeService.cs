using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly UserRepository _userRepository;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _userRepository = new UserRepository();
        }
        public List<Employee> GetAllEmployees() 
        {
            return _employeeRepository.GetAllEmployees();
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }
        public void AddEmployee(Employee employee, string username, string password)
        {
            try
            {
                var existingUser = _userRepository.GetUserByUsername(username);
                if (existingUser != null)
                {
                    throw new Exception("Username đã tồn tại. Vui lòng chọn username khác.");
                }
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = password, 
                    Role = "Employee"
                };

                _userRepository.AddUser(newUser);
                var createdUser = _userRepository.GetUserByUsername(username);
                if (createdUser == null)
                {
                    throw new Exception("Không thể tạo User mới.");
                }
                employee.UserId = createdUser.UserId;
                _employeeRepository.AddEmployee(employee);

                Console.WriteLine("Thêm Employee thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                throw;
            }
        }
        
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
        }
        public void SaveEmployeeAvatar(int employeeId, byte[] avatarData)
        {
            _employeeRepository.SaveEmployeeAvatar(employeeId, avatarData);
        }
        public byte[] GetEmployeeAvatar(int employeeId)
        {
            return _employeeRepository.GetEmployeeAvatar(employeeId);
        }
        public List<Employee> SearchEmployees(string keyword)
        {
            return _employeeRepository.SearchEmployees(keyword);
        }
    }
}
