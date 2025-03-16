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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepo;
        public DepartmentService()
        {
            _departmentRepo = new DepartmentRepository();
        }

        public List<Department> GetAllDepartment()
        {
            return _departmentRepo.GetAllDepartment();
        }
    }
}
