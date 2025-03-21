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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly EmployeeManagementContext _context;

        public LeaveRequestRepository()
        {
            _context = new EmployeeManagementContext();
        }

        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                _context.LeaveRequests.Add(leaveRequest);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public List<LeaveRequest> GetAllLeaveRequests()
        {
            try
            {
                return _context.LeaveRequests.Include(lr => lr.Employee).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LeaveRequest> GetPendingLeaveRequests()
        {
            try
            {
                return _context.LeaveRequests.Where(lr => lr.Status == "Pending").Include(lr => lr.Employee).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetUsedLeaveDays(int employeeId, string leaveType)
        {
            try
            {
                var leaveRequests = _context.LeaveRequests
               .Where(lr => lr.EmployeeId == employeeId && lr.LeaveType == leaveType)
               .ToList(); // Chuyển dữ liệu về bộ nhớ trước khi tính toán

                return leaveRequests.Sum(lr => (lr.EndDate.ToDateTime(TimeOnly.MinValue) - lr.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateStatusLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                var existingRequest = _context.LeaveRequests.Find(leaveRequest.LeaveId);
                if (existingRequest != null)
                {
                    existingRequest.Status = leaveRequest.Status;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetLeave()
        {
            return _context.LeaveRequests.Where(lr => lr.Status.Equals("Pending")).ToList().Count();
        }
    }
}
