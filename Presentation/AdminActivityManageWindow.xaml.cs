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
    /// Interaction logic for AdminActivityManageWindow.xaml
    /// </summary>
    public partial class AdminActivityManageWindow : Window
    {
        private List<ActivityLog> activities = new List<ActivityLog>();
        private readonly IActivityService _activityService;
        public AdminActivityManageWindow()
        {
            InitializeComponent();

            _activityService = new ActivityService(); 

            GetAllActivity();
        }

        private void GetAllActivity()
        {
            activities = _activityService.GetActivityLogs();

            ActivityDataGrid.ItemsSource = activities;
        }

        private void sendNotiBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
