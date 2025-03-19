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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic.Service;
using Models.Models;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeNotificationPage.xaml
    /// </summary>
    public partial class EmployeeNotificationPage : Page
    {
        private readonly INotificationService _notificationService;
        private readonly IEmployeeService _employeeService;
        private List<Notification> notifications;
        private User currentUser;
        public EmployeeNotificationPage()
        {
            InitializeComponent();

            _notificationService = new NotificationService();
            _employeeService = new EmployeeService();

            currentUser = (User)App.Current.Properties["user"];

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int userId = currentUser.UserId;
                Employee employee = _employeeService.GetEmployeeByUserId(userId);
                int departmentId = (int)employee.DepartmentId;
                notifications = _notificationService.GetNotificationByEmployeeIdAndDepartmentId(userId, departmentId);

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
    }
}
