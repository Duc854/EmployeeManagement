using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace BusinessLogic.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepo;
        public ActivityService()
        {
            _activityRepo = new ActivityRepository();
        }

        public bool CreateActivityLog(ActivityLog activityLog)
        {
            return _activityRepo.CreateActivityLog(activityLog);
        }

        public List<ActivityLog> GetActivityLogs()
        {
            return _activityRepo.GetActivityLogs();
        }
    }
}
