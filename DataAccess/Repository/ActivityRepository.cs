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
    public class ActivityRepository : IActivityRepository
    {
        private readonly EmployeeManagementContext _context;
        public ActivityRepository()
        {
            _context = new EmployeeManagementContext();
        }

        public bool CreateActivityLog(ActivityLog activityLog)
        {
            try
            {
                _context.ActivityLogs
                    .Add(activityLog);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ActivityLog> GetActivityLogs()
        {
            var activities = _context.ActivityLogs
                .AsNoTracking()
                .ToList();

            return activities;
        }
    }
}
