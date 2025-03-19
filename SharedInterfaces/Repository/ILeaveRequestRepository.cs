using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace SharedInterfaces.Repository
{
    public interface ILeaveRequestRepository
    {
        void AddLeaveRequest(LeaveRequest leaveRequest);

        List<LeaveRequest> GetPendingLeaveRequests();

       void UpdateStatusLeaveRequest(LeaveRequest leaveRequest);

        List<LeaveRequest> GetAllLeaveRequests();

        int GetUsedLeaveDays(int employeeId, string leaveType);

    }
}
