using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using Models.Models;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeManagementWindow.xaml
    /// </summary>
    public partial class EmployeeManagementWindow : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly IActivityService _activityService;
        private User _currentUser;
        public EmployeeManagementWindow()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _activityService = new ActivityService();
            _currentUser = (User)App.Current.Properties["user"];
            LoadEmployees();
        }
        public void LoadEmployees()
        {
            dgEmployee.ItemsSource = _employeeService.GetAllEmployees();
        }
        

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEmployee.SelectedItem is Employee selectedEmployee)
            {
                txtEmployeeID.Text = selectedEmployee.EmployeeId.ToString();
                txtFullName.Text = selectedEmployee.FullName;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployeeWindow = new CreateEmployee();
            createEmployeeWindow.Show();
            Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployee.SelectedItem is Employee selectedEmployee)
            {
                UpdateEmployee updateWindow = new UpdateEmployee(selectedEmployee.EmployeeId);
                updateWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeID.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int EmployeeId = int.Parse(txtEmployeeID.Text);
            var employee = _employeeService.GetEmployeeById(EmployeeId);

            if (employee != null)
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _employeeService.DeleteEmployee(EmployeeId);
                    LoadEmployees();
                    ClearEmployeeFields();
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    _activityService.CreateActivityLog(new ActivityLog
                    {
                        UserId = _currentUser.UserId,
                        Action = $"Delete user {employee.FullName}",
                        Timestamp = DateTime.Now,
                    });
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy độc giả!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearEmployeeFields()
        {
            txtEmployeeID.Clear();
            txtFullName.Clear();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã đăng xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            System.Diagnostics.Process.Start(appPath);
            Application.Current.Shutdown();
        }

        private void txtSearchEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_employeeService == null)
            {
                return;
            }

            string searchText = txtSearchEmployee.Text?.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadEmployees();
                return;
            }

            var result = _employeeService.SearchEmployees(searchText);
            dgEmployee.ItemsSource = result ?? new List<Employee>();
        }
    }
}
