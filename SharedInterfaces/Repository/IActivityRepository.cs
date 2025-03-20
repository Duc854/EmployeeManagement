using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface IActivityRepository
    {
        public List<ActivityLog> GetActivityLogs();
        public bool CreateActivityLog(ActivityLog activityLog);
    }
}
