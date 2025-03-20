using System;
using System.Collections.Generic;
using System.Data;
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
using Models.Models;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ReportEmployeeWindow.xaml
    /// </summary>
    public partial class ReportEmployeeWindow : Window
    {
        private readonly IReportService _reportService;
        private readonly IActivityService _activityService;
        private User _currentUser;

        public ReportEmployeeWindow()
        {
            InitializeComponent();
            _reportService = new ReportService(new ReportRepository());
            _activityService = new ActivityService();
            _currentUser = (User)App.Current.Properties["user"];

            LoadAllEmployees(); 
        }

        private void LoadAllEmployees()
        {
            var result = _reportService.GetAllEmployees(); 
            dgResult.ItemsSource = result;
        }
        
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string department = txtDepartment.Text.Trim();
            string gender = ((ComboBoxItem)cbGender.SelectedItem)?.Content?.ToString();
            string minSalaryText = txtMinSalary.Text.Trim();
            string maxSalaryText = txtMaxSalary.Text.Trim();
            DateTime? startDate = dpStartDate.SelectedDate;

            decimal? minSalary = decimal.TryParse(minSalaryText, out var min) ? min : (decimal?)null;
            decimal? maxSalary = decimal.TryParse(maxSalaryText, out var max) ? max : (decimal?)null;

            var result = _reportService.GetEmployeesFiltered(department, gender, minSalary, maxSalary, startDate);
            dgResult.ItemsSource = result;

            _activityService.CreateActivityLog(new ActivityLog
            {
                UserId = _currentUser.UserId,
                Action = $"Filter user",
                Timestamp = DateTime.Now,
            });
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtDepartment.Text = string.Empty;
            cbGender.SelectedIndex = -1;
            txtMinSalary.Text = string.Empty;
            txtMaxSalary.Text = string.Empty;
            dpStartDate.SelectedDate = null;

            LoadAllEmployees();
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
