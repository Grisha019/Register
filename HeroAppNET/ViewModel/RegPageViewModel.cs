using HeroAppNET.Services.AuthenticationService;
using HeroAppNET.ViewModel.Base;
using HeroAppNET.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;


namespace HeroAppNET.ViewModel
{
    public partial class RegPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegPageViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register()
        {
            if (_authService.UserExists(Login, Email))
            {
                MessageBox.Show("Пользователь с таким логином или почтой уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new UserModel
            {
                Name = Name,
                Login = Login,
                Email = Email,
                Password = Password
            };

            _authService.Register(user);

            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очистка формы
            Name = string.Empty;
            Login = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
