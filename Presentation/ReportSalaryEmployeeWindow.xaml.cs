using System;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ReportSalaryEmployeeWindow.xaml
    /// </summary>
    public partial class ReportSalaryEmployeeWindow : Window
    {
        private readonly IReportService _reportService;
        public ReportSalaryEmployeeWindow()
        {
            InitializeComponent();
            _reportService = new ReportService(new ReportRepository());

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
        }

        private void btnSalaryByQuarter_Click(object sender, RoutedEventArgs e)
        {
            var result = _reportService.GetSalaryStatisticsByQuarter();
            dgSalaryStatistics.ItemsSource = result;

            dgSalaryStatistics.Columns[1].Header = "Quý";

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
