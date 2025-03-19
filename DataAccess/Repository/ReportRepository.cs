using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees
            .Include(e => e.Department)
            .ToList();

        }

        public List<Employee> GetEmployeesFiltered(string departmentName, string gender, decimal? minSalary, decimal? maxSalary, DateTime? startDate)
        {
            DateOnly? startDateOnly = startDate.HasValue ? DateOnly.FromDateTime(startDate.Value) : null;

            return _context.Employees
                .Include(e => e.Department)
                .Where(e =>
                    (string.IsNullOrEmpty(departmentName) || e.Department.DepartmentName.Contains(departmentName)) &&
                    (string.IsNullOrEmpty(gender) || e.Gender == gender) &&
                    (!minSalary.HasValue || e.Salary >= minSalary) &&
                    (!maxSalary.HasValue || e.Salary <= maxSalary) &&
                    (!startDate.HasValue || e.StartDate >= startDateOnly))
                .ToList();
        }

        public List<Tuple<string, int>> GetEmployeeStatisticsByDepartment()
        {
            return _context.Employees
                .Include(e => e.Department)
                .ToList() // chuyển sang in-memory LINQ
                .GroupBy(e => e.Department.DepartmentName)
                .Select(g => Tuple.Create(g.Key, g.Count()))
                .OrderByDescending(t => t.Item2)
                .ToList();
        }



        public List<Tuple<string, int>> GetEmployeeStatisticsByPosition()
        {
            return _context.Employees
                .ToList()
                .GroupBy(e => e.Position)
                .Select(g => Tuple.Create(g.Key, g.Count()))
                .OrderByDescending(t => t.Item2)
                .ToList();
        }


        public List<Tuple<string, int>> GetEmployeeStatisticsByGender()
        {
            return _context.Employees
                .ToList()
                .GroupBy(e => e.Gender)
                .Select(g => Tuple.Create(g.Key, g.Count()))
                .OrderByDescending(t => t.Item2)
                .ToList();
        }


        public List<Tuple<int, int, decimal>> GetSalaryStatisticsByMonth()
        {
            return _context.Salaries
                .ToList() // chuyển sang in-memory LINQ
                .GroupBy(s => new { s.PayDate.Year, s.PayDate.Month })
                .Select(g => Tuple.Create(
                    g.Key.Year,
                    g.Key.Month,
                    g.Sum(s => (decimal?)s.TotalSalary ?? 0m)
                ))
                .OrderBy(g => g.Item1).ThenBy(g => g.Item2)
                .ToList();
        }

        public List<Tuple<int, int, decimal>> GetSalaryStatisticsByQuarter()
        {
            return _context.Salaries
                .ToList()
                .GroupBy(s => new
                {
                    s.PayDate.Year,
                    Quarter = (s.PayDate.Month - 1) / 3 + 1
                })
                .Select(g => Tuple.Create(g.Key.Year, g.Key.Quarter, g.Sum(s => (decimal?)s.TotalSalary) ?? 0m))
                .OrderBy(g => g.Item1).ThenBy(g => g.Item2)
                .ToList();
        }



    }
}