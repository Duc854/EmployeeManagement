using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface ISalaryService
    {
        List<Salary> GetAllSalary();
        Salary GetSalaryById(int id);
        void AddSalary(Salary salary);
        void UpdateSalary(Salary salary);
        void DeleteSalary(int id);
        Salary GetSalaryByEmployeeId(int employeeId);


    }
}
