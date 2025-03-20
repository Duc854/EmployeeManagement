﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
using BusinessLogic.Service;
using DataAccess.Repository;
using Microsoft.Win32;
using SharedInterfaces.Service;
using ClosedXML.Excel;
using Models.Models;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ReportSalaryEmployeeWindow.xaml
    /// </summary>
    public partial class ReportSalaryEmployeeWindow : Window
    {
        private readonly IReportService _reportService;
        private readonly IActivityService _activityService;
        private User _currentUser;
        public ReportSalaryEmployeeWindow()
        {
            InitializeComponent();
            _reportService = new ReportService(new ReportRepository());

            _activityService = new ActivityService();
            _currentUser = (User)App.Current.Properties["user"];

            // 👉 Tự động hiển thị thống kê theo phòng ban khi khởi động
            LoadSalaryStatisticsByMonth();
        }

        private void LoadSalaryStatisticsByMonth()
        {
            var result = _reportService.GetSalaryStatisticsByMonth();
            dgSalaryStatistics.ItemsSource = result;
        }


        private void btnSalaryByMonth_Click(object sender, RoutedEventArgs e)
        {
            var result = _reportService.GetSalaryStatisticsByMonth();
            dgSalaryStatistics.ItemsSource = result;

            dgSalaryStatistics.Columns[1].Header = "Tháng";

            _activityService.CreateActivityLog(new ActivityLog
            {
                UserId = _currentUser.UserId,
                Action = $"Filter User by Salary by Month",
                Timestamp = DateTime.Now,
            });
        }

        private void btnSalaryByQuarter_Click(object sender, RoutedEventArgs e)
        {
            var result = _reportService.GetSalaryStatisticsByQuarter();
            dgSalaryStatistics.ItemsSource = result;

            dgSalaryStatistics.Columns[1].Header = "Quý";

            _activityService.CreateActivityLog(new ActivityLog
            {
                UserId = _currentUser.UserId,
                Action = $"Filter User by Salary by Quarter",
                Timestamp = DateTime.Now,
            });
        }
        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgSalaryStatistics.ItemsSource == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Chuyển DataGrid.ItemsSource về DataTable
                DataView dataView = dgSalaryStatistics.ItemsSource as DataView;
                if (dataView == null)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DataTable dataTable = dataView.ToTable();

                // Mở hộp thoại chọn nơi lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "ThongKeLuongNhanVien.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(dataTable, "Thống kê lương");
                        worksheet.Columns().AdjustToContents(); // Tự chỉnh độ rộng cột

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Xuất file Excel thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    _activityService.CreateActivityLog(new ActivityLog
                    {
                        UserId = _currentUser.UserId,
                        Action = $"Export report by salary to excel",
                        Timestamp = DateTime.Now,
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã đăng xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            System.Diagnostics.Process.Start(appPath);
            Application.Current.Shutdown();
        }
    }
}
