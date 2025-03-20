using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface IDataMergeService
    {
        public List<int> MergeUsersAsync(List<User> users);
        public void MergeEmployeesAsync(List<Employee> employees);
        public List<int> MergeDepartmentsAsync(List<Department> departments);
    }
}
