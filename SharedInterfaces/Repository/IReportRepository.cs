using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface IReportRepository
    {
        List<Employee> GetAllEmployees();

        List<Employee> GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate);

        List<Tuple<string, int>> GetEmployeeStatisticsByDepartment();

        List<Tuple<string, int>> GetEmployeeStatisticsByPosition();

        List<Tuple<string, int>> GetEmployeeStatisticsByGender();

        List<Tuple<int, int, decimal>> GetSalaryStatisticsByMonth();

        List<Tuple<int, int, decimal>> GetSalaryStatisticsByQuarter();
    }
}