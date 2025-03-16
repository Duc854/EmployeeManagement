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
            //if (lstActions.SelectedItem is ListBoxItem selectedItem)
            //{
            //    string action = selectedItem.Content.ToString();
            //    if (action == "Quản lý người dùng")
            //    {
            //        CustomerManagement customerManagement = new CustomerManagement();
            //        customerManagement.Show();
            //    }
            //    if (action == "Quản lý phòng")
            //    {
            //        RoomManagementView roomManagementView = new RoomManagementView();
            //        roomManagementView.Show();
            //    }
            //    if (action == "Quản lý đặt phòng")
            //    {
            //        BookingManagementView bookingManagementView = new BookingManagementView();
            //        bookingManagementView.Show();
            //    }
            //}
        }

        private void FilterReports_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
