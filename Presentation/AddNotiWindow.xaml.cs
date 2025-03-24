using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for AddNotiWindow.xaml
    /// </summary>
    public partial class AddNotiWindow : Window
    {
        private TextBox txtTitle = new TextBox();
        private TextBox txtMessage = new TextBox();
        private ComboBox receivedComboBox = new ComboBox();
        private ComboBox departmentComboBox = new ComboBox();
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly INotificationService _notiService;
        public AddNotiWindow()
        {
            InitializeComponent();

            _userService = new UserService();
            _departmentService = new DepartmentService();
            _notiService = new NotificationService();
            _employeeService = new EmployeeService();

            CreateForm();
        }

        private void CreateForm()
        {
            CreateTwoInputBox();

            //

            createReceivedComboBoxLabel();
            CreateDepartmentComboBoxLabel();

            //

            Button addBtn = new Button
            {
                Content = "Send",
            };
            addBtn.Click += SendNotification;
            FormPanel.Children.Add(addBtn);
        }

        private void CreateTwoInputBox()
        {
            var titleLabel = new Label
            {
                Content = "Title",
                Margin = new Thickness(5, 10, 5, 2)
            };
            txtTitle = new TextBox
            {
                Margin = new Thickness(5, 0, 5, 10),
                Name = "txtTitle"
            };
            FormPanel.Children.Add(titleLabel);
            FormPanel.Children.Add(txtTitle);

            var messageLabel = new Label
            {
                Content = "Message",
                Margin = new Thickness(5, 10, 5, 2)
            };
            txtMessage = new TextBox
            {
                Margin = new Thickness(5, 0, 5, 10),
                Name = "txtMessage"
            };
            FormPanel.Children.Add(messageLabel);
            FormPanel.Children.Add(txtMessage);
        }

        private void createReceivedComboBoxLabel()
        {
            var receivedComboBoxLabel = new Label
            {
                Content = "Select Employee",
                Margin = new Thickness(5, 10, 5, 2)
            };
            receivedComboBox = new ComboBox
            {
                Margin = new Thickness(5, 0, 5, 10),
                Name = "receivedComboBox"
            };

            List<Employee> employees = _employeeService.GetAllEmployee();
            var userOptions = employees
                .Select(x => new NotiUserOption
                {
                    UserId = x.EmployeeId,
                    UserName = x.FullName
                })
                .ToList();
            userOptions.Insert(0, new NotiUserOption
            {
                UserId = -1,
                UserName = "Không gửi đến người dùng nào"
            });
            receivedComboBox.ItemsSource = userOptions;
            receivedComboBox.SelectedItem = userOptions
                .FirstOrDefault(x => x.UserName == "Không gửi đến người dùng nào");

            FormPanel.Children.Add(receivedComboBoxLabel);
            FormPanel.Children.Add(receivedComboBox);
        }

        private void CreateDepartmentComboBoxLabel()
        {
            var departmentComboBoxLabel = new Label
            {
                Content = "Select Department",
                Margin = new Thickness(5, 10, 5, 2)
            };
            departmentComboBox = new ComboBox
            {
                Margin = new Thickness(5, 0, 5, 10),
                Name = "receivedComboBox"
            };

            List<Department> departments = _departmentService.GetAllDepartment();
            var departmentOptions = departments
                .Select(x => new NotiDepartmentOption
                {
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName
                })
                .ToList();
            departmentOptions.Insert(0, new NotiDepartmentOption
            {
                DepartmentId = -2,
                DepartmentName = "Gửi tới tất cả"
            });
            departmentOptions.Insert(0, new NotiDepartmentOption
            {
                DepartmentId = -1,
                DepartmentName = "Không gửi đến phòng ban nào"
            });
            departmentComboBox.ItemsSource = departmentOptions;
            departmentComboBox.SelectedItem = departmentOptions
                .FirstOrDefault(x => x.DepartmentName == "Không gửi đến phòng ban nào");

            FormPanel.Children.Add(departmentComboBoxLabel);
            FormPanel.Children.Add(departmentComboBox);
        }

        private void SendNotification(object sender, RoutedEventArgs e)
        {
            User currentUser = (User)App.Current.Properties["user"];
            NotiUserOption receivedUser = (NotiUserOption)receivedComboBox.SelectedItem;
            NotiDepartmentOption receivedDepartment = (NotiDepartmentOption)departmentComboBox.SelectedItem;

            if (receivedUser.UserId == -1 && receivedDepartment.DepartmentId == -1)
            {
                MessageBox.Show("Bạn phải chọn nơi để gửi tin nhắn");
                return;
            }

            if (receivedUser.UserId == -1 && receivedDepartment.DepartmentId == -2)
            {
                receivedUser.UserId = -1;
                receivedDepartment.DepartmentId = -1;
            }

            Notification sentNotification = new Notification
            {
                Title = txtTitle.Text,
                Message = txtMessage.Text,
                SenderId = currentUser.UserId,
                ReceiverId = receivedUser.UserId != -1 ? receivedUser.UserId : null,
                DepartmentId = receivedDepartment.DepartmentId != -1 ? receivedDepartment.DepartmentId : null,
                SentDate = DateTime.Now
            };

            //MessageBox.Show($"Title: {txtTitle.Text}, Message: {txtMessage.Text}, SenderId: {currentUser.UserId}, ReceivedUser: {receivedUser.UserId}, Department: {receivedDepartment.DepartmentId}, Date: {DateTime.Now}");

            var sendNotiResult = _notiService.CreateANotification(sentNotification);

            if (sendNotiResult == "Gửi thông báo thành công")
                MessageBox.Show(sendNotiResult, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(sendNotiResult, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            this.Close();
        }
    }
}
