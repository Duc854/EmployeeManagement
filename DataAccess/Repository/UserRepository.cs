using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedInterfaces;

namespace DataAccess.Repository
{
    public class UserRepository : I
    {
        private readonly EmployeeManagementContext _employeeManagementContext;

        public UserRepository()
        {
            _employeeManagementContext = new EmployeeManagementContext();
        }
    }
}
