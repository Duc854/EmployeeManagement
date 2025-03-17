using BusinessLogic.Service;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeHomePage.xaml
    /// </summary>
    public partial class EmployeeHomePage : Window
    {
        private User User;
        private readonly AttendanceService _attendanceService;
        private readonly NotificationService _notificationService;

        public EmployeeHomePage()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
            _attendanceService = new AttendanceService();
            LoadData();
        }

        private void LoadData()
        {
                User = Application.Current.Properties["user"] as User;
                txtDepartment.Text = User.Employee.Department.DepartmentName;
                txtFullName.Text = User.Employee.FullName;
                txtPosition.Text = User.Employee.Position;
                byte[] avatarBytes = User.Employee.Avatar;
                if (avatarBytes != null)
                {
                    AvatarImage.Source = LoadImage(avatarBytes);
                }
        }


        private void ActionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            welcomeTxt.Visibility = Visibility.Hidden;

            if (lstActions.SelectedItem is ListBoxItem selectedItem)
            {
                string action = selectedItem.Content.ToString();
                if (action == "Thông báo nội bộ")
                {
                    MainFrame.Navigate(new EmployeeNotificationPage());
                }
                if (action == "Thông tin cá nhân")
                {
                    EmployeeProfile window = new EmployeeProfile(User.Employee.EmployeeId);
                    window.Show();
                }
            }
        }

        private void DailyAttendance(object sender, RoutedEventArgs e)
        {
            string noti = _attendanceService.DailyAttendace(User.UserId);
            MessageBox.Show($"{noti}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn đã đăng xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            string appPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            System.Diagnostics.Process.Start(appPath);
            Application.Current.Shutdown();
        }

        private BitmapImage LoadImage(byte[] imageData)
        {
            using (var stream = new MemoryStream(imageData))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }
    }

}
