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
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        void SaveEmployeeAvatar(int employeeId, byte[] avatarData);
        byte[] GetEmployeeAvatar(int employeeId);
        List<Employee> SearchEmployees(string keyword);
    }
}
