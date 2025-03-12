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
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }
        public List<Employee> GetAllEmployees() 
        {
            return _employeeRepository.GetAllEmployees();
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
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
