using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class SalaryService : ISalaryService
    {
        private readonly SalaryRepository _salaryRepository;

        public SalaryService()
        {
            _salaryRepository = new SalaryRepository();
        }

        public void AddSalary(Salary salary)
        {
            _salaryRepository.AddSalary(salary);
        }

        public void DeleteSalary(int id)
        {
           _salaryRepository.DeleteSalary(id);
        }

        public List<Salary> GetAllSalary()
        {
           return _salaryRepository.GetAllSalary();
        }

        public Salary GetSalaryById(int id)
        {
          return _salaryRepository.GetSalaryById(id);
        }

        public void UpdateSalary(Salary salary)
        {
          _salaryRepository.UpdateSalary(salary);
        }
    }
}
