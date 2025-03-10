using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly EmployeeManagementContext _context;
        public AttendanceRepository()
        {
            _context = new EmployeeManagementContext();
        }
        public void CheckInAttendance(int employeeId)
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var now = TimeOnly.FromDateTime(DateTime.Now);

                var attendance = _context.Attendances.AsNoTracking()
                    .FirstOrDefault(a => a.EmployeeId == employeeId && a.WorkDate == today);

                if (attendance == null)
                {
                    attendance = new Attendance
                    {
                        EmployeeId = employeeId,
                        WorkDate = today,
                        CheckInTime = now
                    };
                    _context.Attendances.Add(attendance);
                }
                else
                {
                    Console.WriteLine("Bạn đã chấm công hôm nay rồi!");
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CheckInLateAttendance(int employeeId)
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var attendance = _context.Attendances.AsNoTracking()
                    .FirstOrDefault(a => a.EmployeeId == employeeId && a.WorkDate == today);

                if (attendance == null)
                {
                    attendance = new Attendance
                    {
                        EmployeeId = employeeId,
                        WorkDate = today,
                        CheckInTime = null
                    };
                    _context.Attendances.Add(attendance);
                }
                else
                {
                    Console.WriteLine("Bạn đã chấm công hôm nay rồi!");
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Attendance> GetTodayAttendance()
        {
            try
            {
                return _context.Attendances.Where(atd => atd.WorkDate == DateOnly.FromDateTime(DateTime.Now)).ToList();
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
