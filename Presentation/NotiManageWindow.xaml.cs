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
    /// Interaction logic for NotiManageWindow.xaml
    /// </summary>
    public partial class NotiManageWindow : Window
    {
        private readonly INotificationService _notiService;
        public NotiManageWindow()
        {
            InitializeComponent();

            _notiService = new NotificationService();

            GetAllNotifications();
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
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? choosedSentDate = notiDatePicker.SelectedDate;
                List<Notification> notis = new List<Notification>();

                if (choosedSentDate == null)
                {
                    MessageBox.Show("Vui lòng chọn thời gian để tìm thông báo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    notis = _notiService.GetNotificationBySentDate(choosedSentDate.Value);
                    NotisDataGrid.ItemsSource = notis;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
