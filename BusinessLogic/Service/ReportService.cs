using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public DataTable GetAllEmployees()
        {
            return _reportRepository.GetAllEmployees();
        }

        public DataTable GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate)
        {
            return _reportRepository.GetEmployeesFiltered(departmentName, gender, minSalary, maxSalary, startDate);
        }

        public DataTable GetEmployeeStatisticsByDepartment()
        {
            return _reportRepository.GetEmployeeStatisticsByDepartment();
        }

        public DataTable GetEmployeeStatisticsByGender()
        {
            return _reportRepository.GetEmployeeStatisticsByGender();
        }
        public DataTable GetEmployeeStatisticsByPosition()
        {
            return _reportRepository.GetEmployeeStatisticsByPosition();
        }

        public DataTable GetSalaryStatisticsByMonth()
        {
            return _reportRepository.GetSalaryStatisticsByMonth();
        }
        public DataTable GetSalaryStatisticsByQuarter()
        {
            return _reportRepository.GetSalaryStatisticsByQuarter();
        }
    }
}
