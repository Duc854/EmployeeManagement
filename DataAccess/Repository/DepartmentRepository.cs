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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementContext _context;
        public DepartmentRepository()
        {
            _context = new EmployeeManagementContext();
        }

        public List<Department> GetAllDepartment()
        {
            try
            {
                var departments = _context.Departments
                    .AsNoTracking()
                    .ToList();

                return departments;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
