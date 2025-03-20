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
using BusinessLogic.Service;
using DataAccess.Repository;
using SharedInterfaces.Service;
using ClosedXML.Excel;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Data;
using Microsoft.Win32;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for ReportEmployeeNumberWindow.xaml
    /// </summary>
    public partial class ReportEmployeeNumberWindow : Window
    {
        private readonly IReportService _reportService;

        public ReportEmployeeNumberWindow()
        {
            InitializeComponent();
            _reportService = new ReportService(new ReportRepository());

            LoadStatisticsByDepartment();
        }

        private void LoadStatisticsByDepartment()
        {
            var result = _reportService.GetEmployeeStatisticsByDepartment();
            dgStatistics.ItemsSource = result;
        }

        private void btnStatisticsByDepartment_Click(object sender, RoutedEventArgs e)
        {
            var data = _reportService.GetEmployeeStatisticsByDepartment();
            dgStatistics.ItemsSource = data;
        }

        private void btnStatisticsByGender_Click(object sender, RoutedEventArgs e)
        {
            var data = _reportService.GetEmployeeStatisticsByGender();
            dgStatistics.ItemsSource = data;
        }

        private void btnStatisticsByPosition_Click(object sender, RoutedEventArgs e)
        {
            var data = _reportService.GetEmployeeStatisticsByPosition();
            dgStatistics.ItemsSource = data;
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgStatistics.ItemsSource == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var items = dgStatistics.ItemsSource.Cast<dynamic>().ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("Nhóm thống kê");
                dt.Columns.Add("Số lượng");

                foreach (var item in items)
                {
                    dt.Rows.Add(item.Item1.ToString(), item.Item2.ToString());
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "ThongKeNhanVien.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add(dt, "Thống kê");
                        worksheet.Columns().AdjustToContents();
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


        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgStatistics.ItemsSource == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var items = dgStatistics.ItemsSource.Cast<dynamic>().ToList();

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF file (*.pdf)|*.pdf",
                    FileName = "ThongKeNhanVien.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var document = new Document();
                    var section = document.AddSection();

                    var title = section.AddParagraph("BÁO CÁO THỐNG KÊ NHÂN VIÊN");
                    title.Format.Alignment = ParagraphAlignment.Center;
                    title.Format.Font.Size = 16;
                    title.Format.Font.Bold = true;
                    title.Format.SpaceAfter = "1cm";

                    var table = section.AddTable();
                    table.Borders.Width = 0.75;

                    table.AddColumn("6cm");
                    table.AddColumn("4cm");

                    var headerRow = table.AddRow();
                    headerRow.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
                    headerRow.Cells[0].AddParagraph("Nhóm thống kê");
                    headerRow.Cells[1].AddParagraph("Số lượng");

                    foreach (var item in items)
                    {
                        var row = table.AddRow();
                        row.Cells[0].AddParagraph(item.Item1.ToString());
                        row.Cells[1].AddParagraph(item.Item2.ToString());
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
