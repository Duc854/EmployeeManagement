﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.Repository
{
    public interface IAttendanceRepository
    {
        public void CheckInAttendance(int employeeId);
        public void CheckInLateAttendance(int employeeId);
        public List<Attendance> GetTodayAttendance();
    }
}
