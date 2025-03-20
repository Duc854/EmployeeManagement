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
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentService()
        {
            _departmentRepository = new DepartmentRepository();
        }
        public void AddDepartment(Department department)
        {
            _departmentRepository.AddDepartment(department);
        }

        public void DeleteDepartment(int id)
        {
            _departmentRepository.DeleteDepartment(id);
        }

        public List<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments();
        }

        public Department GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentById(id);
        }

        public void UpdateDepartment(Department department)
        {
            _departmentRepository.UpdateDepartment(department);
        }

        public List<Department> GetAllDepartment()
        {
            return _departmentRepository.GetAllDepartment();
        }

        public int GetNumDepartment()
        {
            return _departmentRepository.GetAllDepartment().Count;
        }
    }
}
