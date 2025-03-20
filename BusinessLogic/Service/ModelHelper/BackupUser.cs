using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace BusinessLogic.Service.ModelHelper
{
    public class BackupUser
    {
        public List<User> Users { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
        public List<Salary> Salaries { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
