using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeLeaveStatus.xaml
    /// </summary>
    public partial class EmployeeLeaveStatus : Window
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly int _employeeId;

        private const int MAX_ANNUAL_LEAVE = 12;
        private const int MAX_SICK_LEAVE = 5;
        private const int MAX_UNPAID_LEAVE = 999;

        public EmployeeLeaveStatus(int employeeId)
        {
            InitializeComponent();
            _leaveRequestRepo = new LeaveRequestRepository();
            _employeeId = employeeId;
            LoadLeaveBalance();
        }

        private void LoadLeaveBalance()
        {
            var usedAnnualLeave = _leaveRequestRepo.GetUsedLeaveDays(_employeeId, "Annual Leave");
            var usedSickLeave = _leaveRequestRepo.GetUsedLeaveDays(_employeeId, "Sick Leave");
            var usedUnpaidLeave = _leaveRequestRepo.GetUsedLeaveDays(_employeeId, "Unpaid Leave");

            txtAnnualLeave.Text = $"Còn lại: {Math.Max(0, MAX_ANNUAL_LEAVE - usedAnnualLeave)} ngày";
            txtSickLeave.Text = $"Còn lại: {Math.Max(0, MAX_SICK_LEAVE - usedSickLeave)} ngày";
            txtUnpaidLeave.Text = $"Còn lại: {MAX_UNPAID_LEAVE - usedUnpaidLeave} ngày";
        }
    }
}
