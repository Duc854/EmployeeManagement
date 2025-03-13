using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Repository
{
    public interface IReportRepository
    {
        DataTable GetAllEmployees();
        DataTable GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate);
        DataTable GetEmployeeStatisticsByDepartment();
        DataTable GetEmployeeStatisticsByGender();
        DataTable GetEmployeeStatisticsByPosition();
        DataTable GetSalaryStatisticsByMonth();
        DataTable GetSalaryStatisticsByQuarter();
    }
}
