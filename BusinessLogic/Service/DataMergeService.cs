using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class DataMergeService : IDataMergeService
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public DataMergeService()
        {
            _departmentRepo = new DepartmentRepository();
            _userRepo = new UserRepository();
            _employeeRepo = new EmployeeRepository();
        }

        public void MergeDepartmentsAsync(List<Department> departments)
        {

            foreach (var department in departments)
            {
                var existingDepartment = _departmentRepo.GetDepartmentById(department.DepartmentId);

                if (existingDepartment != null)
                {
                    existingDepartment.DepartmentName = department.DepartmentName;
                    _departmentRepo.UpdateDepartment(existingDepartment);
                }
                else
                {
                    _departmentRepo.AddDepartment(department);
                }
            }
        }

        public void MergeEmployeesAsync(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var existingEmployee = _employeeRepo.GetEmployeeById(employee.EmployeeId);


                if (existingEmployee != null)
                {
                    existingEmployee.FullName = employee.FullName;
                    existingEmployee.BirthDate = employee.BirthDate;
                    existingEmployee.Gender = employee.Gender;
                    existingEmployee.Address = employee.Address;
                    existingEmployee.PhoneNumber = employee.PhoneNumber;
                    existingEmployee.DepartmentId = employee.DepartmentId;
                    existingEmployee.Position = employee.Position;
                    existingEmployee.Salary = employee.Salary;
                    existingEmployee.StartDate = employee.StartDate;
                    existingEmployee.Avatar = employee.Avatar;

                    _employeeRepo.UpdateEmployee(existingEmployee);
                }
                else
                {
                    _employeeRepo.AddEmployee(employee);
                }
            }
        }


        public void MergeUsersAsync(List<User> users)
        {
            foreach (var user in users)
            {
                var existingUser = _userRepo.GetUserByUsername(user.Username);

                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    existingUser.PasswordHash = user.PasswordHash;
                    existingUser.Role = user.Role;

                    _userRepo.UpdateUser(existingUser);
                }
                else
                {
                    _userRepo.AddUser(user);
                }
            }
        }

    }
}
