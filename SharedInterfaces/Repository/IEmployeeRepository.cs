﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        void SaveEmployeeAvatar(int employeeId, byte[] avatarData);
        byte[] GetEmployeeAvatar(int employeeId);
        List<Employee> SearchEmployees(string keyword);
        public List<Employee> GetAllEmployee();
        public Employee GetEmployeeByUserId(int userId);
        Task DeleteAllEmployees();
        Task<Employee> AddEmployeeAsync(Employee employee);
    }
}
