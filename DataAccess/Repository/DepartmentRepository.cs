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
        public List<Department> GetAllDepartments()
        {
            return _context.Departments.Include(d => d.Employees)
                .Include(d => d.Notifications).ToList();
        }
        public Department GetDepartmentById(int id)
        {
            return _context.Departments.Find(id);
        }
        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        public void UpdateDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            _context.Departments.Remove(department);
            _context.SaveChanges();
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
