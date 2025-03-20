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
using BusinessLogic.Service.ModelHelper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharedInterfaces.Service;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for ManageBackupWindow.xaml
    /// </summary>
    public partial class ManageBackupWindow : Window
    {
        private readonly IBackupAndRestoreService _backupService;
        public ManageBackupWindow()
        {
            InitializeComponent();

            _backupService = new BackupAndRestoreService();
        }

        private async void backupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = ".json";

                bool? result = saveFileDialog.ShowDialog();
                if (result == true)
                {
                    string filePath = saveFileDialog.FileName;

                    await _backupService.BackupData(filePath);

                    MessageBox.Show($"Sao lưu dữ liệu thành công! File được lưu tại: {filePath}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn nơi lưu trữ file.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xảy ra lỗi khi xuất file {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void restoreBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                openFileDialog.DefaultExt = ".json";

                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    string filePath = openFileDialog.FileName;

                    await _backupService.RestoreDataAsync(filePath);

                    MessageBox.Show($"Dữ liệu đã được phục hồi thành công từ file: {filePath}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn file để phục hồi.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Xảy ra lỗi khi phục hồi dữ liệu: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }
        }
    }
}
