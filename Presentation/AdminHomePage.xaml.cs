using BusinessLogic.Service;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Window
    {
        public AttendanceService _attendanceService;
        public AdminHomePage()
        {
            InitializeComponent();
            _attendanceService = new AttendanceService();
            LoadData();
        }

        private void LoadData()
        {
            EmployeeCountText.Text = "0"; 
            DepartmentCountText.Text = "0";
            LateAttendanceText.Text = "0";
            PendingLeaveText.Text = "0";
            OtherMetricsText.Text = "0";
        }
        private void ActionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstActions.SelectedItem is ListBoxItem selectedItem)
            {
                string action = selectedItem.Content.ToString();
                if (action == "Quản lý thông báo")
                {
                    NotiManageWindow window = new NotiManageWindow();
                    window.Show();
                }
                if (action == "Quản lý nhân viên")
                {
                    EmployeeManagementWindow window = new EmployeeManagementWindow();
                    window.Show();
                }
                if (action == "Quản lý phòng ban")
                {
                    DepartmentWindow window = new DepartmentWindow();
                    window.Show();
                }
                if (action == "Thống kê theo nhóm")
                {
                    ReportEmployeeNumberWindow window = new ReportEmployeeNumberWindow();
                    window.Show();
                }
                if (action == "Lọc nhân viên")
                {
                    ReportEmployeeWindow window = new ReportEmployeeWindow();
                    window.Show();
                }
                if (action == "Thống kê lương")
                {
                    ReportSalaryEmployeeWindow window = new ReportSalaryEmployeeWindow();
                    window.Show();
                }
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
