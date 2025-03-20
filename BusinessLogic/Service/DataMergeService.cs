using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Service.ModelHelper;
using DataAccess.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class DataMergeService
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

        public List<int> MergeDepartmentsAsync(List<Department> departments)
        {
            var departmentIds = new List<int>();

            foreach (var department in departments)
            {
                var existingDepartment = _departmentRepo.GetDepartmentById(department.DepartmentId);

                if (existingDepartment != null)
                {
                    existingDepartment.DepartmentName = department.DepartmentName;
                    _departmentRepo.UpdateDepartment(existingDepartment);
                    departmentIds.Add(existingDepartment.DepartmentId); 
                }
                else
                {
                    var addedDepartment = _departmentRepo.AddDepartmentBackup(new Department
                    {
                        DepartmentName = department.DepartmentName
                    });
                    departmentIds.Add(addedDepartment.DepartmentId);
                }
            }

            return departmentIds;
        }

        public void MergeEmployeesAsync(List<Employee> employees, List<NameAndId> userNames, List<NameAndId> departmentNames)
        {
            foreach (var employee in employees)
            {
                var existingEmployee = _employeeRepo.GetEmployeeById(employee.EmployeeId);

                NameAndId user = userNames.FirstOrDefault(name => name.Name == employee.User.Username);
                NameAndId department = departmentNames.FirstOrDefault(name => name.Name == employee.Department.DepartmentName); 

                if (existingEmployee != null)
                {
                    existingEmployee.FullName = employee.FullName;
                    existingEmployee.BirthDate = employee.BirthDate;
                    existingEmployee.Gender = employee.Gender;
                    existingEmployee.Address = employee.Address;
                    existingEmployee.PhoneNumber = employee.PhoneNumber;
                    existingEmployee.DepartmentId = department.Id;
                    existingEmployee.UserId = user.Id; 
                    existingEmployee.Position = employee.Position;
                    existingEmployee.Salary = employee.Salary;
                    existingEmployee.StartDate = employee.StartDate;
                    existingEmployee.Avatar = employee.Avatar;

                    _employeeRepo.UpdateEmployee(existingEmployee);
                }
                else
                {
                    _employeeRepo.AddEmployee(new Employee
                    {
                        FullName = employee.FullName,
                        BirthDate = employee.BirthDate,
                        Gender = employee.Gender,
                        Address = employee.Address,
                        PhoneNumber = employee.PhoneNumber,
                        DepartmentId = department.Id,
                        UserId = user.Id,
                        Position = employee.Position,
                        Salary = employee.Salary,

                    });
                }
            }
        }


        public List<NameAndId> MergeUsersAsync(List<User> users)
        {
            var userIds = new List<NameAndId>();

            foreach (var user in users)
            {
                var existingUser = _userRepo.GetUserByUsername(user.Username);

                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    existingUser.PasswordHash = user.PasswordHash;
                    existingUser.Role = user.Role;

                    _userRepo.UpdateUser(existingUser);
                    //userIds.Add(new NameAndId
                    //{
                    //    Name = 
                    //}); 
                }
                else
                {
                    var addedUser = _userRepo.AddUserBackup(new User
                    {
                        Username = user.Username,
                        PasswordHash = user.PasswordHash,
                        Role = user.Role
                    });
                    //userIds.Add(addedUser.UserId);
                }
            }

            return userIds;
        }

    }
}
