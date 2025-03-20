using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace BusinessLogic.Service.ModelHelper
{
    public class BackupUser
    {
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
