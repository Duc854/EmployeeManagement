using DataAccess.Repository;
using SharedInterfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;
        public AttendanceService() 
        {
            _attendanceRepository = new AttendanceRepository();
        }
        public string DailyAttendace(int employeeId)
        {
            var now = TimeOnly.FromDateTime(DateTime.Now);
            try
            {
                if (now > new TimeOnly(8, 0))
                {
                    _attendanceRepository.CheckInLateAttendance(employeeId);
                    return "Đã quá giờ chấm công vui lòng liên hệ với quản lý!";
                }
                _attendanceRepository.CheckInAttendance(employeeId);
                return "Chấm công thành công!";
            }
            catch (Exception ex)
            {
                return "Xảy ra lỗi trong quá trình xử lý dữ liệu! \n Vui lòng thử lại sau";
            }
        }
    }
}
