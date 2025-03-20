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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AttendanceReportWindow.xaml
    /// </summary>
    public partial class AttendanceReportWindow : Window
    {

        private readonly AttendanceRepository _attendanceRepo;

        public AttendanceReportWindow()
        {
            InitializeComponent();
            _attendanceRepo = new AttendanceRepository();

            // Load danh sách tháng & năm
            cbMonth.ItemsSource = Enumerable.Range(1, 12);
            cbMonth.SelectedItem = DateTime.Now.Month;

            cbYear.ItemsSource = Enumerable.Range(DateTime.Now.Year - 5, 6);
            cbYear.SelectedItem = DateTime.Now.Year;

            LoadReport();
        }
        private void LoadReport()
        {
            int month = (int)cbMonth.SelectedItem;
            int year = (int)cbYear.SelectedItem;

            dgReport.ItemsSource = _attendanceRepo.GetMonthlyAttendance(month, year);
        }


        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadReport();
        }

    }



}
