using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Presentation.Helper;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for NotiManageWindow.xaml
    /// </summary>
    public partial class NotiManageWindow : Window
    {
        private readonly INotificationService _notiService;
        private readonly IActivityService _activityService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private User _currentUser;
        public NotiManageWindow()
        {
            InitializeComponent();

            _notiService = new NotificationService();
            _activityService = new ActivityService();
            _departmentService = new DepartmentService();
            _userService = new UserService();
            _employeeService = new EmployeeService();
            _currentUser = (User)App.Current.Properties["user"];

            GetOptions();
            GetAllNotifications();
        }

        private void GetOptions()
        {
            List<Department> departments = _departmentService.GetAllDepartment();
            var departmentOptions = departments
                .Select(x => new NotiDepartmentOption
                {
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName
                })
                .ToList();
            departmentIdComboBox.ItemsSource = departmentOptions;

            List<Employee> employees = _employeeService.GetAllEmployee();
            var userOptions = employees
                .Select(x => new NotiUserOption
                {
                    UserId = x.EmployeeId,
                    UserName = x.FullName
                })
                .ToList();
            receiverIdComboBox.ItemsSource = userOptions;

            List<User> admins = _userService.GetAllAdmin();
            var adminOptions = admins
                .Select(x => new NotiUserOption
                {
                    UserId = x.UserId,
                    UserName = x.Username
                });
            senderIdComboBox.ItemsSource = adminOptions;
        }

        private void GetAllNotifications()
        {

            var notis = _notiService.GetAllNotification();

            if (notis.Count > 0)
            {
                NotisDataGrid.ItemsSource = notis;
            }
        }

        private async void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var addNotiWindow = new AddNotiWindow();
            var tcs = new TaskCompletionSource<bool>();

            addNotiWindow.Closed += (sender, e) =>
            {
                tcs.SetResult(true);
            };
            addNotiWindow.Show();

            await tcs.Task;
            GetAllNotifications();

            _activityService.CreateActivityLog(new ActivityLog
            {
                UserId = _currentUser.UserId,
                Action = "Send a notification",
                Timestamp = DateTime.Now,
            });
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? choosedSentDate = notiDatePicker.SelectedDate;
                var receivedUser = receiverIdComboBox.SelectedItem;
                var receivedDepartment = departmentIdComboBox.SelectedItem;
                var senderUser = senderIdComboBox.SelectedItem;
                List<Notification> notis = new List<Notification>();

                if (choosedSentDate == null &&
                    !(receivedUser is NotiUserOption receiver) && 
                    !(receivedDepartment is NotiDepartmentOption department) &&
                    !(senderUser is NotiUserOption adminSender))
                {
                    MessageBox.Show("Vui lòng chọn tiêu chí để tìm thông báo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    int receiverId = 0;
                    int departmentId = 0;
                    int senderId = 0;

                    if (receivedUser is NotiUserOption tmpReceiver)
                        receiverId = tmpReceiver.UserId;
                    if (receivedDepartment is NotiDepartmentOption tmpDepartment)
                        departmentId = tmpDepartment.DepartmentId;
                    if (senderUser is NotiUserOption tmpSender)
                        senderId = tmpSender.UserId;

                    notis = _notiService.GetNotificationBySentDate(choosedSentDate, senderId, receiverId, departmentId);
                    NotisDataGrid.ItemsSource = notis;
                }


                _activityService.CreateActivityLog(new ActivityLog
                {
                    UserId = _currentUser.UserId,
                    Action = "Search notification",
                    Timestamp = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            Notification notification = button?.DataContext as Notification;

            if (notification != null)
            {
                await _notiService.DeleteNoti(notification.NotificationId);

                MessageBox.Show("Đã xóa thông báo thành công");

                _activityService.CreateActivityLog(new ActivityLog
                {
                    UserId = _currentUser.UserId,
                    Action = $"Delete notification {notification.NotificationId}",
                    Timestamp = DateTime.Now,
                });

                GetAllNotifications();
            } 
        }
    }
}
