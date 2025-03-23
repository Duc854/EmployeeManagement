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
        private readonly IActivityRepository _activityLogRepo;
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ISalaryRepository _salaryRepo;
        private readonly INotificationRepository _notificationRepo;
        public BackupAndRestoreService()
        {
            _departmentRepo = new DepartmentRepository();
            _userRepo = new UserRepository();
            _employeeRepo = new EmployeeRepository();
            _activityLogRepo = new ActivityRepository();
            _attendanceRepo = new AttendanceRepository();
            _leaveRequestRepo = new LeaveRequestRepository();
            _salaryRepo = new SalaryRepository();
            _notificationRepo = new NotificationRepository();
        }
        public async Task BackupData(string filePath)
        {
            var backupData = new BackupUser
            {
                Users = _userRepo.GetAllUser(),
                Departments = _departmentRepo.GetAllDepartments(),
                Employees = _employeeRepo.GetAllEmployees(),
                ActivityLogs = _activityLogRepo.GetActivityLogs(),
                Attendances = _attendanceRepo.GetAttendances(),
                LeaveRequests = _leaveRequestRepo.GetAllLeaveRequests(),
                Salaries = _salaryRepo.GetAllSalary(),
                Notifications = _notificationRepo.GetAllNotification()
            };

            var jsonData = JsonConvert.SerializeObject(backupData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                Formatting = Formatting.Indented 
            });
            await File.WriteAllTextAsync(filePath, jsonData);
        }

        public async Task RestoreDataAsync(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            var backupData = JsonConvert.DeserializeObject<BackupUser>(jsonData);

            await _attendanceRepo.DeleteAllAttendances();
            await _leaveRequestRepo.DeleteAllLeaveRequests();
            await _salaryRepo.DeleteAllSalaries();
            await _notificationRepo.DeleteAllNotifications();
            await _activityLogRepo.DeleteAllActivityLogs();

            await _employeeRepo.DeleteAllEmployees();
            await _departmentRepo.DeleteAllDepartments();
            await _userRepo.DeleteAllUsers();

            var userIdMapping = new Dictionary<int, int>();
            foreach (var user in backupData.Users)
            {
                int tmpId = user.UserId;
                user.UserId = 0;
                var newUser = _userRepo.AddUserBackup(user);
                userIdMapping[tmpId] = newUser.UserId;
            }

            var departmentIdMapping = new Dictionary<int, int>();
            foreach (var department in backupData.Departments)
            {
                int tmpId = department.DepartmentId;
                department.DepartmentId = 0;
                var newDepartment = _departmentRepo.AddDepartmentBackup(department);
                departmentIdMapping[tmpId] = newDepartment.DepartmentId;
            }

            var employeeIdMapping = new Dictionary<int, int>();
            foreach (var employee in backupData.Employees)
            {
                employee.UserId = userIdMapping[employee.UserId];
                if (employee.DepartmentId.HasValue)
                {
                    employee.DepartmentId = departmentIdMapping[employee.DepartmentId.Value];
                }
                var newEmployee = await _employeeRepo.AddEmployeeAsync(employee);
                employeeIdMapping[employee.EmployeeId] = newEmployee.EmployeeId;
            }

            foreach (var activityLog in backupData.ActivityLogs)
            {
                activityLog.UserId = userIdMapping[activityLog.UserId];
                await _activityLogRepo.AddActivityLogAsync(activityLog);
            }

            foreach (var attendance in backupData.Attendances)
            {
                attendance.EmployeeId = employeeIdMapping[attendance.EmployeeId];
                await _attendanceRepo.AddAttendanceAsync(attendance);
            }

            foreach (var leaveRequest in backupData.LeaveRequests)
            {
                leaveRequest.EmployeeId = employeeIdMapping[leaveRequest.EmployeeId];
                await _leaveRequestRepo.AddLeaveRequestAsync(leaveRequest);
            }

            foreach (var salary in backupData.Salaries)
            {
                salary.EmployeeId = employeeIdMapping[salary.EmployeeId];
                await _salaryRepo.AddSalaryAsync(salary);
            }

            foreach (var notification in backupData.Notifications)
            {
                if (notification.SenderId > 0)
                {
                    notification.SenderId = userIdMapping[notification.SenderId];
                }
                if (notification.ReceiverId.HasValue)
                {
                    notification.ReceiverId = employeeIdMapping[notification.ReceiverId.Value];
                }
                if (notification.DepartmentId.HasValue)
                {
                    notification.DepartmentId = departmentIdMapping[notification.DepartmentId.Value];
                }
                await _notificationRepo.AddNotificationAsync(notification);
            }
        }
    }
}
