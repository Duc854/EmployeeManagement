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
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedInterfaces.Repository;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AttendanceManagementWindow.xaml
    /// </summary>
    public partial class AttendanceManagementWindow : Window
    {

        private readonly IAttendanceRepository _attendanceRepo;
        public AttendanceManagementWindow()
        {
            InitializeComponent();
            _attendanceRepo = new AttendanceRepository();
            LoadAttendanceData();
        }

        private void LoadAttendanceData()
        {
            dgAttendance.ItemsSource = _attendanceRepo.GetTodayAttendance();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(txtEmployeeID.Text) || dpWorkDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên và chọn ngày làm việc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

               
                int employeeId;
                if (!int.TryParse(txtEmployeeID.Text, out employeeId))
                {
                    MessageBox.Show("Mã nhân viên phải là số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                TimeOnly? checkInTime = ParseTime(txtCheckInTime.Text);
                TimeOnly? checkOutTime = ParseTime(txtCheckOutTime.Text);

                decimal? overtimeHours = null;
                if (!string.IsNullOrEmpty(txtOvertimeHours.Text))
                {
                    if (!decimal.TryParse(txtOvertimeHours.Text, out decimal tempOvertime))
                    {
                        MessageBox.Show("Giờ làm thêm phải là số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    overtimeHours = tempOvertime;
                }

                // Tạo đối tượng mới
                Attendance newAttendance = new Attendance
                {
                    EmployeeId = employeeId,
                    WorkDate = DateOnly.FromDateTime(dpWorkDate.SelectedDate.Value),
                    CheckInTime = checkInTime,
                    CheckOutTime = checkOutTime,
                    OvertimeHours = overtimeHours,
                    LeaveType = cbLeaveType.Text
                };

                // Thêm vào database
                _attendanceRepo.AddAttendance(newAttendance);

                MessageBox.Show("Thêm chấm công thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // Cập nhật lại danh sách
                LoadAttendanceData();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chấm công: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtEmployeeID.Text) || dpWorkDate.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên và chọn ngày làm việc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtAttendanceID.Text, out int attendanceId))
                {
                    MessageBox.Show("Mã chấm công không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingAttendance = _attendanceRepo.GetManagementById(attendanceId);
                if (existingAttendance == null)
                {
                    MessageBox.Show("Không tìm thấy bản ghi chấm công!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                

                // Lấy dữ liệu từ form
                TimeOnly? checkInTime = ParseTime(txtCheckInTime.Text);
                TimeOnly? checkOutTime = ParseTime(txtCheckOutTime.Text);
                decimal? overtimeHours = ParseOvertimeHours(txtOvertimeHours.Text);
                string leaveType = cbLeaveType.Text;

                // Cập nhật thông tin
                existingAttendance.EmployeeId = int.Parse(txtEmployeeID.Text);
                existingAttendance.WorkDate = DateOnly.FromDateTime(dpWorkDate.SelectedDate.Value);
                existingAttendance.CheckInTime = checkInTime;
                existingAttendance.CheckOutTime = checkOutTime;
                existingAttendance.OvertimeHours = overtimeHours;
                existingAttendance.LeaveType = leaveType;

                // Cập nhật vào database
                _attendanceRepo.UpdateAttendence(existingAttendance);

                MessageBox.Show("Cập nhật chấm công thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAttendanceData();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAttendanceID.Text))
                {
                    MessageBox.Show("Vui lòng chọn bản ghi chấm công để xóa!");
                    return;
                }

                int attendanceId = int.Parse(txtAttendanceID.Text);

                var attendance = _attendanceRepo.GetManagementById(attendanceId);

                if (attendance == null)
                {
                    MessageBox.Show("Không tìm thấy bản ghi chấm công!");
                    return;
                }



                _attendanceRepo.DeleteAttendence(int.Parse(txtAttendanceID.Text));

                MessageBox.Show("Xóa thành công!");
                LoadAttendanceData();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtAttendanceID.Clear();
            txtEmployeeID.Clear();
            dpWorkDate.SelectedDate = null;
            txtCheckInTime.Clear();
            txtCheckOutTime.Clear();
            txtOvertimeHours.Clear();
            cbLeaveType.SelectedIndex = 0;
        }

        private void dgAttendance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAttendance.SelectedItem is Attendance attendance)
            {
                txtAttendanceID.Text = attendance.AttendanceId.ToString();
                txtEmployeeID.Text = attendance.EmployeeId.ToString();
                dpWorkDate.SelectedDate = attendance.WorkDate.ToDateTime(TimeOnly.MinValue);
                txtCheckInTime.Text = attendance.CheckInTime?.ToString();
                txtCheckOutTime.Text = attendance.CheckOutTime?.ToString();
                txtOvertimeHours.Text = attendance.OvertimeHours.ToString();
                cbLeaveType.Text = attendance.LeaveType;
            }
        }

        private TimeOnly? ParseTime(string timeText)
        {
            if (string.IsNullOrWhiteSpace(timeText))
                return null;

            string[] formats = { "H:mm", "HH:mm", "h:mm tt", "hh:mm tt" }; // Hỗ trợ 24h & 12h AM/PM

            if (TimeOnly.TryParseExact(timeText, formats, null, System.Globalization.DateTimeStyles.None, out TimeOnly parsedTime))
            {
                return parsedTime;
            }
            else
            {
                MessageBox.Show($"Giá trị '{timeText}' không hợp lệ! Vui lòng nhập theo định dạng HH:mm hoặc h:mm tt (VD: 08:00 hoặc 8:00 SA).",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private decimal? ParseOvertimeHours(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            if (decimal.TryParse(text, out decimal result))
            {
                return result;
            }
            else
            {
                MessageBox.Show("Giá trị giờ làm thêm không hợp lệ! Vui lòng nhập số hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            AttendanceReportWindow reportWindow = new AttendanceReportWindow();
            reportWindow.ShowDialog();
        }
    }
}
