using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface ISalaryRepository
    {

        List<Salary> GetAllSalary();
        Salary GetSalaryById(int id);
        void AddSalary(Salary salary);
        void UpdateSalary(Salary salary);
        void DeleteSalary(int id);

        Task DeleteAllSalaries();

    }
}
