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
        private List<Notification> notifications;
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
            try
            {
                notifications = _notificationService.HomeNotifications();

                if (notifications == null || notifications.Count == 0)
                {
                    notifications = new List<Notification>
                {
                    new Notification
                    {
                        Title = "Không có thông báo nào để hiển thị",
                        Message = ""
                    }
                };
                }
                NotificationList.ItemsSource = notifications;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông báo: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                notifications = new List<Notification>();
            }
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

        private void NotificationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationList.SelectedItem is Notification selectedNotification)
            {
                DetailTitle.Text = selectedNotification.Title;
                DetailContent.Text = selectedNotification.Message;
                DetailPanel.Visibility = Visibility.Visible;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DetailPanel.Visibility == Visibility.Visible)
            {
                DetailPanel.Visibility = Visibility.Collapsed;
                NotificationList.Visibility = Visibility.Visible;
                NotificationList.SelectedItem = null;
            }
        }

        private void ActionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstActions.SelectedItem is ListBoxItem selectedItem)
            {
                string action = selectedItem.Content.ToString();
                if (action == "Thông tin cá nhân")
                {
                    EmployeeProfile window = new EmployeeProfile(User.Employee.EmployeeId);
                    window.Show();
                }
                //if (action == "Quản lý phòng")
                //{
                //    RoomManagementView roomManagementView = new RoomManagementView();
                //    roomManagementView.Show();
                //}
                //if (action == "Quản lý đặt phòng")
                //{
                //    BookingManagementView bookingManagementView = new BookingManagementView();
                //    bookingManagementView.Show();
                //}
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
