using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeService()
        {
            _employeeRepo = new EmployeeRepository();
        }

        public List<Employee> GetAllEmployee()
        {
            return _employeeRepo.GetAllEmployee();
        }
    }
}
