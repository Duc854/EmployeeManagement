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
using DataAccess.Repository;
using Models.Models;
using SharedInterfaces.Repository;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for LeaveRequestManagementWindow.xaml
    /// </summary>
    public partial class LeaveRequestManagementWindow : Window
    {

        private readonly ILeaveRequestRepository _leaveRequestRepo;

        public LeaveRequestManagementWindow()
        {
            InitializeComponent();
            _leaveRequestRepo = new LeaveRequestRepository();
            LoadPendingRequests();
        }

        private void LoadPendingRequests()
        {
            dgLeaveRequests.ItemsSource = _leaveRequestRepo.GetPendingLeaveRequests();
        }

        private void LoadAllRequests()
        {
            dgLeaveRequests.ItemsSource = _leaveRequestRepo.GetAllLeaveRequests();
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (dgLeaveRequests.SelectedItem is LeaveRequest selectedRequest)
            {
                selectedRequest.Status = "Approved";
                _leaveRequestRepo.UpdateStatusLeaveRequest(selectedRequest);
                MessageBox.Show("Đã phê duyệt đơn nghỉ phép!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPendingRequests();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn nghỉ phép!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            if (dgLeaveRequests.SelectedItem is LeaveRequest selectedRequest)
            {
                selectedRequest.Status = "Rejected";
                _leaveRequestRepo.UpdateStatusLeaveRequest(selectedRequest);
                MessageBox.Show("Đã từ chối đơn nghỉ phép!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPendingRequests();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn nghỉ phép!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPending_Click(object sender, RoutedEventArgs e)
        {
            LoadPendingRequests();

        }

        private void btnAllRequests_Click(object sender, RoutedEventArgs e)
        {
            LoadAllRequests();
        }
    }
}
