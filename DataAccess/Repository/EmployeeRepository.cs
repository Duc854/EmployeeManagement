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

        public List<Employee> GetAllEmployee()
        {
            try
            {
                var employees = _context.Employees
                    .AsNoTracking()
                    .ToList();

                return employees;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
