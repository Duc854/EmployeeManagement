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

        public List<Attendance> GetAttendance()
        {
            try
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                return _context.Attendances.Include(a => a.Employee).Where(a => a.WorkDate == today)
                   .ToList();
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
                return _context.Attendances.Include(a => a.Employee)
                   .ToList();
            }
            catch (Exception ex) {
                throw;
            }
        }

        public void AddAttendance(Attendance attendance)
        {
            try
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }


        }



        public void UpdateAttendence(Attendance attendance)
        {
            try
            {
              _context.Entry(attendance).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteAttendence(int id)
        {
            try
            {
              _context.Attendances.Remove(_context.Attendances.Find(id));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Attendance GetManagementById(int id)
        {
            try
            {
                return _context.Attendances.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu Attendance: " + ex.Message);
            }
        }

        public List<Attendance> GetMonthlyAttendance(int month, int year)
        {

            return _context.Attendances
                .Where(a => a.WorkDate.Year == year && a.WorkDate.Month == month)
                .Include(a => a.Employee) // Load thông tin nhân viên
                .ToList();
        }

        public List<Attendance> GetAttendances()
        {
            return _context.Attendances
                .ToList();
        }

        public async Task DeleteAllAttendances()
        {
            var attendances = await _context.Attendances
                .ToListAsync();
            _context.RemoveRange(attendances);

            await _context.SaveChangesAsync();
        }

        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances
                .AddAsync(attendance);
            await _context.SaveChangesAsync();
        }
    }
    }

