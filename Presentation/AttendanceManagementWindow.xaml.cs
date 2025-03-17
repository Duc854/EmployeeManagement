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
            if (string.IsNullOrEmpty(txtEmployeeID.Text) || dpWorkDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên và chọn ngày làm việc!");
                return;
            }
            TimeOnly? checkInTime = ParseTime(txtCheckInTime.Text);
            TimeOnly? checkOutTime = ParseTime(txtCheckOutTime.Text);


            int attendanceId = int.Parse(txtAttendanceID.Text);

            var attendance = _attendanceRepo.GetManagementById(attendanceId);

            if (attendance == null)
            {
                MessageBox.Show("Không tìm thấy bản ghi chấm công!");
                return;
            }


   

            Attendance newAttendance = new Attendance
            {
                EmployeeId = int.Parse(txtEmployeeID.Text),
                WorkDate = DateOnly.FromDateTime(dpWorkDate.SelectedDate.Value),
                CheckInTime = checkInTime,
                CheckOutTime = checkOutTime,
                OvertimeHours = string.IsNullOrEmpty(txtOvertimeHours.Text) ? null : decimal.Parse(txtOvertimeHours.Text),
                LeaveType = cbLeaveType.Text
            };

            _attendanceRepo.AddAttendance(newAttendance);

            MessageBox.Show("Thêm chấm công thành công!");
            LoadAttendanceData();
            btnReset_Click(sender, e);

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

                int attendanceID = int.Parse(txtAttendanceID.Text);

                var attendance = _attendanceRepo.GetManagementById(attendanceId);

                if (attendance == null)
                {
                    MessageBox.Show("Không tìm thấy bản ghi chấm công!");
                    return;
                }


                TimeOnly? checkInTime = ParseTime(txtCheckInTime.Text);
                TimeOnly? checkOutTime = ParseTime(txtCheckOutTime.Text);


                if (!string.IsNullOrEmpty(txtCheckInTime.Text))
                {
                    if (TimeOnly.TryParseExact(txtCheckInTime.Text, "HH:mm", out TimeOnly parsedCheckIn))
                        checkInTime = parsedCheckIn;
                    else
                    {
                        MessageBox.Show("Giờ vào không hợp lệ! Vui lòng nhập theo định dạng HH:mm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtCheckOutTime.Text))
                {
                    if (TimeOnly.TryParseExact(txtCheckOutTime.Text, "HH:mm", out TimeOnly parsedCheckOut))
                        checkOutTime = parsedCheckOut;
                    else
                    {
                        MessageBox.Show("Giờ ra không hợp lệ! Vui lòng nhập theo định dạng HH:mm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                decimal? overtimeHours = null;
                if (!string.IsNullOrEmpty(txtOvertimeHours.Text))
                {
                    if (decimal.TryParse(txtOvertimeHours.Text, out decimal parsedOvertime))
                        overtimeHours = parsedOvertime;
                    else
                    {
                        MessageBox.Show("Số giờ làm thêm không hợp lệ! Vui lòng nhập số.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                // Cập nhật dữ liệu
                _attendanceRepo.UpdateAttendence(new Attendance
                {
                    AttendanceId = attendanceID,
                    EmployeeId = int.Parse(txtEmployeeID.Text),
                    WorkDate = DateOnly.FromDateTime(dpWorkDate.SelectedDate.Value),
                    CheckInTime = checkInTime,
                    CheckOutTime = checkOutTime,
                    OvertimeHours = overtimeHours,
                    LeaveType = cbLeaveType.Text
                });



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


    }
}
