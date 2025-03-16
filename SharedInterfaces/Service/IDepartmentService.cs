using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface IDepartmentService
    {
        public List<Department> GetAllDepartment();
    }
}
