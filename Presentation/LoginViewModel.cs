using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BusinessLogic.Service;

namespace Presentation
{
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
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object parameter) => !string.IsNullOrWhiteSpace(Username);

        private void ExecuteLogin(object parameter)
        {
            if (parameter is System.Windows.Controls.PasswordBox passwordBox)
            {
                string password = passwordBox.Password;
                var user = _userService.Authenticate(Username, password);

                if (user != null)
                {
                    SessionManager.CurrentUser = user;

                    if (user.Role == "Admin")
                    {
                        new Views.AdminView().Show();
                    }
                    else
                    {
                        new Views.EmployeeView().Show();
                    }

                    Application.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
