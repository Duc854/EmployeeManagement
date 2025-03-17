using BusinessLogic.Service;
using DataAccess.Repository;
using Models.Models;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeProfile.xaml
    /// </summary>
    public partial class EmployeeProfile : Window
    {
        private readonly EmployeeService _employeeService;
        private readonly int _employeeId;

        public EmployeeProfile(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _employeeService = new EmployeeService();
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            Employee? employee = _employeeService.GetEmployeeById(_employeeId);

            if (employee != null)
            {
                txtFullName.Text = employee.FullName;
                txtBirthDate.Text = employee.BirthDate.ToString();
                txtGender.Text = employee.Gender;
                txtPhoneNumber.Text = employee.PhoneNumber;
                txtPosition.Text = employee.Position ?? "Chưa cập nhật";
                txtSalary.Text = employee.Salary?.ToString() ?? "Chưa cập nhật";
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
    }
}
