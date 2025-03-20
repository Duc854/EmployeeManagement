using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Models.Models;
using SharedInterfaces.Service;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IActivityService _activityService;
        private User _currentUser;
        private byte[] avatarData;
        public CreateEmployee()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _departmentService = new DepartmentService();
            _activityService = new ActivityService();
            _currentUser = (User)App.Current.Properties["user"];
            LoadGenderOptions();
            LoadDepartments();
        }
        private void LoadGenderOptions()
        {
            cbGender.ItemsSource = new List<string> { "Male", "Female", "Other" };
        }
        private void LoadDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            cbDepartmentId.ItemsSource = departments;
            cbDepartmentId.DisplayMemberPath = "DepartmentId";
            cbDepartmentId.SelectedValuePath = "DepartmentId";
        }
        private void cbDepartmentId_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbDepartmentId.SelectedItem is Department selectedDepartment)
            {
                txtDepartmentName.Text = selectedDepartment.DepartmentName;
            }
        }
        private void btnUploadAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                avatarData = File.ReadAllBytes(openFileDialog.FileName);
                imgAvatar.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmployee = new Employee
                {
                    FullName = txtFullName.Text,
                    BirthDate = dpBirthDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpBirthDate.SelectedDate.Value) : default(DateOnly),
                    Gender = cbGender.SelectedItem?.ToString(),
                    Address = txtAddress.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    DepartmentId = (int)cbDepartmentId.SelectedValue,
                    Position = txtPosition.Text,
                    Salary = decimal.Parse(txtSalary.Text),
                    StartDate = dpStartDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpStartDate.SelectedDate.Value) : default(DateOnly),
                    Avatar = avatarData
                };

                _employeeService.AddEmployee(newEmployee, txtUsername.Text, txtPassword.Text);
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                _activityService.CreateActivityLog(new ActivityLog
                {
                    UserId = _currentUser.UserId,
                    Action = $"Create user {txtFullName.Text}",
                    Timestamp = DateTime.Now,
                });

                this.Hide();
                EmployeeManagementWindow employeeManagementWindow = new EmployeeManagementWindow();
                employeeManagementWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagementWindow employeeManagementWindow = new EmployeeManagementWindow();
            employeeManagementWindow.Show();
            Close();
        }
    }
}
