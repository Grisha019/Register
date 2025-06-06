using HeroAppNET.Services.AuthenticationService;
using HeroAppNET.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HeroAppNET.Models.Utilty;
using HeroAppNET.ViewModel.Base;

namespace HeroAppNET.ViewModel
{
    public class RegPageViewModel : ViewModelBase
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

        public IAsyncRelayCommand RegisterCommand { get; }

        public RegPageViewModel()
        {
            RegisterCommand = new AsyncRelayCommand(RegisterAsync);
        }

        private async Task RegisterAsync()
        {
            if (await _authService.UserExistsAsync(Login, Email))
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

            await _authService.RegisterAsync(user);

            MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

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
