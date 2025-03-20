using System;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;

namespace Presentation
{
    public partial class LeaveRequestWindow : Window
    {
        private readonly ILeaveRequestRepository _context;
        private readonly int _employeeID; 

        public LeaveRequestWindow(int employeeID)
        {
            InitializeComponent();
            _context = new LeaveRequestRepository();
            _employeeID = employeeID;
          
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbLeaveType.SelectedItem == null || dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = _employeeID,
                    LeaveType = ((ComboBoxItem)cbLeaveType.SelectedItem).Tag.ToString(),
                    StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value),
                    Status = "Pending"
                };

                _context.AddLeaveRequest(leaveRequest);

                MessageBox.Show("Đã gửi đơn nghỉ phép!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
