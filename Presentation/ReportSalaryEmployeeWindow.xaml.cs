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
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

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

                DataView dataView = dgSalaryStatistics.ItemsSource as DataView;
                if (dataView == null)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DataTable dataTable = dataView.ToTable();

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

        private void btnExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgSalaryStatistics.ItemsSource == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var items = dgSalaryStatistics.ItemsSource.Cast<dynamic>().ToList();

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF file (*.pdf)|*.pdf",
                    FileName = "ThongKeLuongNhanVien.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var document = new Document();
                    var section = document.AddSection();

                    var paragraph = section.AddParagraph("BÁO CÁO THỐNG KÊ LƯƠNG NHÂN VIÊN");
                    paragraph.Format.Alignment = ParagraphAlignment.Center;
                    paragraph.Format.Font.Size = 16;
                    paragraph.Format.Font.Bold = true;
                    paragraph.Format.SpaceAfter = "1cm";

                    var table = section.AddTable();
                    table.Borders.Width = 0.75;

                    table.AddColumn("3cm");
                    table.AddColumn("3cm");
                    table.AddColumn("6cm");

                    var headerRow = table.AddRow();
                    headerRow.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
                    headerRow.Cells[0].AddParagraph("Năm");
                    headerRow.Cells[1].AddParagraph(dgSalaryStatistics.Columns[1].Header.ToString());
                    headerRow.Cells[2].AddParagraph("Lương tổng");

                    foreach (var item in items)
                    {
                        var row = table.AddRow();
                        row.Cells[0].AddParagraph(item.Item1.ToString());
                        row.Cells[1].AddParagraph(item.Item2.ToString());
                        row.Cells[2].AddParagraph(item.Item3.ToString());
                    }

                    var renderer = new PdfDocumentRenderer(true);
                    renderer.Document = document;
                    renderer.RenderDocument();
                    renderer.PdfDocument.Save(saveFileDialog.FileName);

                    MessageBox.Show("Xuất file PDF thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
