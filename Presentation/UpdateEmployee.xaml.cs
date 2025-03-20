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
using DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Models.Models;
using SharedInterfaces.Repository;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Window
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserRepository _userRepo;
        private Employee _employee;
        private readonly IActivityService _activityService;
        private User _currentUser;

        private string? _avatarPath = null;
        public UpdateEmployee(int employeeId)
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _departmentService = new DepartmentService();
            _userRepo = new UserRepository();
            _activityService = new ActivityService();
            _currentUser = (User)App.Current.Properties["user"];
            LoadGenderOptions();
            LoadEmployeeData(employeeId);
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
        private void LoadEmployeeData(int employeeId)
        {
            _employee = _employeeService.GetEmployeeById(employeeId);
            if (_employee == null)
            {
                MessageBox.Show("Nhân viên không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            var user = _userRepo.GetUserById(_employee.UserId);
            txtUsername.Text = user?.Username;
            txtPassword.Text = user?.PasswordHash;
            txtFullName.Text = _employee.FullName;
            dpBirthDate.SelectedDate = _employee.BirthDate.ToDateTime(TimeOnly.MinValue);
            dpStartDate.SelectedDate = _employee.StartDate.ToDateTime(TimeOnly.MinValue);
            cbGender.SelectedItem = _employee.Gender;
            txtAddress.Text = _employee.Address;
            txtPhoneNumber.Text = _employee.PhoneNumber;
            cbDepartmentId.SelectedValue = _employee.DepartmentId;
            txtPosition.Text = _employee.Position;
            txtSalary.Text = _employee.Salary.ToString();
            txtDepartmentName.Text = _departmentService.GetDepartmentById(_employee.DepartmentId ?? 0)?.DepartmentName;

            if (_employee.Avatar != null && _employee.Avatar.Length > 0)
            {
                using (var ms = new MemoryStream(_employee.Avatar))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    imgAvatar.Source = image;
                }
            }
        }
        private void btnSelectAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _avatarPath = openFileDialog.FileName;
                imgAvatar.Source = new BitmapImage(new Uri(_avatarPath));
            }
        }
        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _employee.FullName = txtFullName.Text;
                _employee.BirthDate = DateOnly.FromDateTime(dpBirthDate.SelectedDate.Value);
                _employee.Gender = cbGender.Text;
                _employee.Address = txtAddress.Text;
                _employee.PhoneNumber = txtPhoneNumber.Text;
                _employee.DepartmentId = (int)cbDepartmentId.SelectedValue;
                _employee.Position = txtPosition.Text;
                _employee.Salary = decimal.Parse(txtSalary.Text);
                _employee.StartDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value);

                if (!string.IsNullOrEmpty(_avatarPath))
                {
                    _employee.Avatar = File.ReadAllBytes(_avatarPath);
                }

                string? newUsername = txtUsername.Text;
                string? newPassword = txtPassword.Text;

                _employeeService.UpdateEmployee(_employee, newUsername, newPassword);

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                _activityService.CreateActivityLog(new ActivityLog
                {
                    UserId = _currentUser.UserId,
                    Action = $"Update user {txtFullName.Text}",
                    Timestamp = DateTime.Now,
                });

                this.Hide();
                EmployeeManagementWindow employeeManagementWindow = new EmployeeManagementWindow();
                employeeManagementWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
