using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using DataAccess.Repository;
using SharedInterfaces.Repository;
using BusinessLogic.Service.ModelHelper;
using SharedInterfaces.Service;
using Models.Models;

namespace BusinessLogic.Service
{
    public class BackupAndRestoreService : IBackupAndRestoreService
    {
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public BackupAndRestoreService()
        {
            _departmentRepo = new DepartmentRepository();
            _userRepo = new UserRepository();
            _employeeRepo = new EmployeeRepository();
        }
        public async Task BackupData(string filePath)
        {
            List<Department> departments = _departmentRepo.GetAllDepartments();
            List<User> users = _userRepo.GetAllUser();
            List<Employee> employees = _employeeRepo.GetAllEmployees();

            var backup = new BackupUser
            {
                Departments = departments,
                Users = users,
                Employees = employees
            };

            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
            };

            var jsonData = JsonConvert.SerializeObject(backup, jsonSettings);

            await File.WriteAllTextAsync(filePath, jsonData);
        }

        public async Task RestoreData(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
