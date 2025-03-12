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
using Models.Models;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentWindow()
        {
            InitializeComponent();
            _departmentService = new DepartmentService();
            LoadDepartments();
        }
        public void LoadDepartments()
        {
            dgDepartment.ItemsSource = _departmentService.GetAllDepartments();
        }
        private void dgDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartment.SelectedItem is Department selectedDepartment)
            {
                txtDepartmentID.Text = selectedDepartment.DepartmentId.ToString();
                txtDepartmentName.Text = selectedDepartment.DepartmentName;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDepartments();
        }
        private void ClearDepartmentFields()
        {
            txtDepartmentID.Text = "";
            txtDepartmentName.Text = "";
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtDepartmentName.Text))
            {
                MessageBox.Show("Hãy nhập tên phòng ban");
                return;
            }
            var department = new Department
            {
                DepartmentName = txtDepartmentName.Text
            };
            _departmentService.AddDepartment(department);
            LoadDepartments();
            ClearDepartmentFields();
            MessageBox.Show("Thêm phòng ban thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDepartmentID.Text))
            {
                MessageBox.Show("Hãy chọn phòng ban cần cập nhật");
                return;
            }
            int departmentId = int.Parse(txtDepartmentID.Text);
            var department = _departmentService.GetDepartmentById(departmentId);
            if(department == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban cần cập nhật");
                return;
            }
            department.DepartmentName = txtDepartmentName.Text;
            _departmentService.UpdateDepartment(department);
            LoadDepartments();
            ClearDepartmentFields();
            MessageBox.Show("Cập nhật phòng ban thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtDepartmentID.Text))
            {
                MessageBox.Show("Hãy chọn phòng ban cần xóa");
                return;
            }
            int departmentId = int.Parse(txtDepartmentID.Text);
            var department = _departmentService.GetDepartmentById(departmentId);
            if (department == null)
            {
                MessageBox.Show("Không tìm thấy phòng ban cần xóa");
                return;
            }
            _departmentService.DeleteDepartment(departmentId);
            LoadDepartments();
            ClearDepartmentFields();
            MessageBox.Show("Xóa phòng ban thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã đăng xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            Login loginWindow = new Login();
            loginWindow.Show();
            Close();
        }
    }
}
