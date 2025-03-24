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
                // Kiểm tra xem người dùng đã nhập đầy đủ thông tin chưa
                if (cbLeaveType.SelectedItem == null || dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string leaveType = ((ComboBoxItem)cbLeaveType.SelectedItem).Tag?.ToString() ?? "";
                DateOnly startDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value);
                DateOnly endDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value);

                // Kiểm tra ngày bắt đầu không lớn hơn ngày kết thúc
                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra số ngày nghỉ phép
                int requestedDays = (endDate.DayNumber - startDate.DayNumber) + 1;
                if (requestedDays <= 0)
                {
                    MessageBox.Show("Số ngày nghỉ phải lớn hơn 0!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Kiểm tra số ngày nghỉ còn lại của nhân viên
                int maxLeaveDays = leaveType switch
                {
                    "Annual Leave" => 12,  // Nghỉ phép năm tối đa 12 ngày
                    "Sick Leave" => 5,      // Nghỉ ốm tối đa 5 ngày
                    "Unpaid Leave" => 999,  // Nghỉ không lương không giới hạn
                    _ => 0
                };

                int usedLeaveDays = _context.GetUsedLeaveDays(_employeeID, leaveType);
                if (usedLeaveDays + requestedDays > maxLeaveDays)
                {
                    MessageBox.Show($"Bạn đã vượt quá số ngày nghỉ còn lại! Còn lại: {maxLeaveDays - usedLeaveDays} ngày.",
                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Nếu mọi dữ liệu hợp lệ, tiến hành tạo đơn nghỉ phép
                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = _employeeID,
                    LeaveType = leaveType,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = "Pending"
                };

                _context.AddLeaveRequest(leaveRequest);

                MessageBox.Show("Đã gửi đơn nghỉ phép thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
