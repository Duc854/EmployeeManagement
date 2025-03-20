using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Service
{
    public interface IActivityService
    {
        public List<ActivityLog> GetActivityLogs();
        public bool CreateActivityLog(ActivityLog activityLog);
    }
}
