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
    /// Interaction logic for SalaryManagementWindow.xaml
    /// </summary>
    public partial class SalaryManagementWindow : Window
    {
        private readonly ISalaryService _salaryService;
        public SalaryManagementWindow()
        {
            InitializeComponent();
            _salaryService = new SalaryService();
            LoadSalary();
        }

         public void LoadSalary()
        {
            dgSalaries.ItemsSource = _salaryService.GetAllSalary();
        }

        private void dgSalaries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSalaries.SelectedItem is Salary selectedSalary)
            {
                txtSalaryID.Text = selectedSalary.SalaryId.ToString();
                txtEmployeeID.Text = selectedSalary.EmployeeId.ToString();
                txtBasicSalary.Text = selectedSalary.BasicSalary.ToString();
                txtAllowance.Text = selectedSalary.Allowance?.ToString();
                txtBonus.Text = selectedSalary.Bonus?.ToString();
                txtDeduction.Text = selectedSalary.Deduction?.ToString();
                txtTotalSalary.Text = selectedSalary.TotalSalary.ToString();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmployeeID.Text) || string.IsNullOrEmpty(txtBasicSalary.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int employeeId = int.Parse(txtEmployeeID.Text);

                // Kiểm tra xem nhân viên đã có bảng lương chưa
                var existingSalary = _salaryService.GetSalaryByEmployeeId(employeeId);
                if (existingSalary != null)
                {
                    MessageBox.Show("Nhân viên này đã có bảng lương. Không thể thêm mới!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Salary newSalary = new Salary
                {
                    EmployeeId = employeeId,
                    BasicSalary = decimal.Parse(txtBasicSalary.Text),
                    Allowance = string.IsNullOrEmpty(txtAllowance.Text) ? 0 : decimal.Parse(txtAllowance.Text),
                    Bonus = string.IsNullOrEmpty(txtBonus.Text) ? 0 : decimal.Parse(txtBonus.Text),
                    Deduction = string.IsNullOrEmpty(txtDeduction.Text) ? 0 : decimal.Parse(txtDeduction.Text),
                    PayDate = DateOnly.FromDateTime(dpPayDate.SelectedDate.Value)
                };

                _salaryService.AddSalary(newSalary);
                LoadSalary();
                Reset_Click(sender, e);
                MessageBox.Show("Thêm lương thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmployeeID.Text) || string.IsNullOrEmpty(txtBasicSalary.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int salaryId = int.Parse(txtSalaryID.Text);
                var salary = _salaryService.GetSalaryById(salaryId);

                if (salary == null)
                {
                    MessageBox.Show("Không tìm thấy ID cần cập nhật", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                salary.BasicSalary = decimal.Parse(txtBasicSalary.Text);
                salary.Allowance = string.IsNullOrEmpty(txtAllowance.Text) ? 0 : decimal.Parse(txtAllowance.Text);
                salary.Bonus = string.IsNullOrEmpty(txtBonus.Text) ? 0 : decimal.Parse(txtBonus.Text);
                salary.Deduction = string.IsNullOrEmpty(txtDeduction.Text) ? 0 : decimal.Parse(txtDeduction.Text);
                salary.PayDate = dpPayDate.SelectedDate.HasValue ? DateOnly.FromDateTime(dpPayDate.SelectedDate.Value) : salary.PayDate;

            
                _salaryService.UpdateSalary(salary);

                LoadSalary();
                Reset_Click(sender, e);
                MessageBox.Show("Cập nhật lương thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSalaryID.Text)) { 
                MessageBox.Show("Hãy chọn mức lương  cần xóa");
                return;
            }

            int salaryId = int.Parse(txtSalaryID.Text);
            var salary = _salaryService.GetSalaryById(salaryId);

            if (salary == null)
            {
                MessageBox.Show("Không tìm thấy ID cần cập xóa");
                return;
            }

            _salaryService.DeleteSalary(salaryId);
            LoadSalary();
            Reset_Click(sender, e);


        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {

            LoadSalary();
            txtSalaryID.Text = "";
            txtEmployeeID.Text = "";
            txtBasicSalary.Text = "";
            txtAllowance.Text = "";
            txtBonus.Text = "";
            txtDeduction.Text = "";
        }
    }
}
