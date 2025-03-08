using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BusinessLogic.Service;
using Models.Models;
using System.Windows.Controls;

namespace Presentation;
    public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public ICommand LoginCommand { get; }

    private readonly UserService _userService;

    public LoginViewModel()
    {
        _userService = new UserService();
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    private void ExecuteLogin(object parameter)
    {
        if (parameter is PasswordBox passwordBox)
        {
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = _userService.Authenticate(Username, password);

            if (user != null)
            {

                if (user.Role == "Admin")
                {
                    MessageBox.Show($"Đăng nhập (Admin) thành công! Chào mừng, {user.Username}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    new AdminHomePage().Show();
                }
                else
                {
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng, {user.Username}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    new EmployeeHomePage().Show();
                }

                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
