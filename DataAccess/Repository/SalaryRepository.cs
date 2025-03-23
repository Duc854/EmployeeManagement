using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;

namespace DataAccess.Repository
{
    public class SalaryRepository : ISalaryRepository
    {

        private readonly EmployeeManagementContext _contex;

        public SalaryRepository()
        {
            _contex = new EmployeeManagementContext();
        }

        public void AddSalary(Salary salary)
        {
            
            try
            {
                _contex.Salaries.Add(salary);
                _contex.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddSalaryAsync(Salary salary)
        {
            await _contex.Salaries
                .AddAsync(salary);
            await _contex.AddRangeAsync();
        }

        public async Task DeleteAllSalaries()
        {
            var leaves = await _contex.Salaries
                .ToListAsync();
            _contex.RemoveRange(leaves);

            await _contex.SaveChangesAsync();
        }

        public void DeleteSalary(int id)
        {
            try
            {
                var salary = _contex.Salaries.Find(id);
                _contex.Salaries.Remove(salary);
                _contex.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Salary> GetAllSalary()
        {
            try
            {
                return _contex.Salaries.Include(x => x.Employee).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

      

        public Salary GetSalaryById(int id)
        {
            try
            {

                return _contex.Salaries.Find(id); 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public void UpdateSalary(Salary salary)
        {

            try
            {
                _contex.Entry(salary).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _contex.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
