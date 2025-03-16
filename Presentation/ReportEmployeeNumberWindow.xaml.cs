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

            // 👉 Tự động hiển thị thống kê theo phòng ban khi khởi động
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
    }
}
