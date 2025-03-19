using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        void AddEmployee(Employee employee, string? username, string? password);
        void UpdateEmployee(Employee employee, string? newUsername, string? newPassword);
        void DeleteEmployee(int employeeId);
        void SaveEmployeeAvatar(int employeeId, byte[] avatarData);
        byte[] GetEmployeeAvatar(int employeeId);
        List<Employee> SearchEmployees(string keyword);

        public List<Employee> GetAllEmployee();
        public Employee GetEmployeeByUserId(int userId);
    }
}
