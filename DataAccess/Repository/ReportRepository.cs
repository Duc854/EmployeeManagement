using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;
using SharedInterfaces.Repository;

namespace DataAccess.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly EmployeeManagementContext _context;

        public ReportRepository()
        {
            _context = new EmployeeManagementContext();
        }
        public DataTable GetAllEmployees()
        {
            var query = _context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FullName,
                    e.Gender,
                    e.Position,
                    Department = e.Department.DepartmentName,
                    e.Salary,
                    e.StartDate
                }).ToList();

            return ToDataTable(query);
        }

        public DataTable GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate)
        {
            DateOnly? startDateOnly = startDate.HasValue ? DateOnly.FromDateTime(startDate.Value) : null;

            var query = _context.Employees
                .Where(e =>
                    (string.IsNullOrEmpty(departmentName) || e.Department.DepartmentName.Contains(departmentName)) &&
                    (string.IsNullOrEmpty(gender) || e.Gender == gender) &&
                    (!minSalary.HasValue || e.Salary >= minSalary) &&
                    (!maxSalary.HasValue || e.Salary <= maxSalary) &&
                    (!startDateOnly.HasValue || e.StartDate >= startDateOnly))
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FullName,
                    Department = e.Department.DepartmentName,
                    e.Position,
                    e.Gender,
                    e.Salary,
                    e.StartDate
                })
                .ToList();

            return ToDataTable(query);
        }

        public DataTable GetEmployeeStatisticsByDepartment()
        {
            var query = _context.Employees
                .GroupBy(e => e.Department.DepartmentName)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalEmployees = g.Count()
                })
                .ToList();

            return ToDataTable(query);
        }

        public DataTable GetEmployeeStatisticsByPosition()
        {
            var query = _context.Employees
                .GroupBy(e => e.Position)
                .Select(g => new
                {
                    Position = g.Key,
                    TotalEmployees = g.Count()
                })
                .ToList();

            return ToDataTable(query);
        }

        public DataTable GetEmployeeStatisticsByGender()
        {
            var query = _context.Employees
                .GroupBy(e => e.Gender)
                .Select(g => new
                {
                    Gender = g.Key,
                    TotalEmployees = g.Count()
                })
                .ToList();

            return ToDataTable(query);
        }

        public DataTable GetSalaryStatisticsByMonth()
        {
            var query = _context.Salaries
                .GroupBy(s => new { s.PayDate.Year, s.PayDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSalaryPaid = g.Sum(s => s.TotalSalary)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            return ToDataTable(query);
        }

        public DataTable GetSalaryStatisticsByQuarter()
        {
            var query = _context.Salaries
                .GroupBy(s => new
                {
                    s.PayDate.Year,
                    Quarter = (s.PayDate.Month - 1) / 3 + 1
                })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Quarter,
                    TotalSalaryPaid = g.Sum(s => s.TotalSalary)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Quarter)
                .ToList();

            return ToDataTable(query);
        }

        // Helper method: Convert List<dynamic> to DataTable
        private DataTable ToDataTable<T>(List<T> data)
        {
            var dt = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (var item in data)
            {
                var values = props.Select(p => p.GetValue(item, null)).ToArray();
                dt.Rows.Add(values);
            }

            return dt;
        }
    }
}
