using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
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
        public List<Employee> GetAllEmployees()
        {
            return _reportRepository.GetAllEmployees();
        }

        public List<Employee> GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate)
        {
            return _reportRepository.GetEmployeesFiltered(departmentName, gender, minSalary, maxSalary, startDate);
        }

        public List<Tuple<string, int>> GetEmployeeStatisticsByDepartment()
        {
            return _reportRepository.GetEmployeeStatisticsByDepartment();
        }

        public List<Tuple<string, int>> GetEmployeeStatisticsByGender()
        {
            return _reportRepository.GetEmployeeStatisticsByGender();
        }

        public List<Tuple<string, int>> GetEmployeeStatisticsByPosition()
        {
            return _reportRepository.GetEmployeeStatisticsByPosition();
        }

        public List<Tuple<int, int, decimal>> GetSalaryStatisticsByMonth()
        {
            return _reportRepository.GetSalaryStatisticsByMonth();
        }

        public List<Tuple<int, int, decimal>> GetSalaryStatisticsByQuarter()
        {
            return _reportRepository.GetSalaryStatisticsByQuarter();
        }
    }
}
