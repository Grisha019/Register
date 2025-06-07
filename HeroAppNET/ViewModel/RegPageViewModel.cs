using CommunityToolkit.Mvvm.Input;
using HeroAppNET.Models;
using HeroAppNET.Services.AuthenticationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace HeroAppNET.ViewModel
{
    public class RegPageViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        private string _name = string.Empty;
        private string _login = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _footSize = string.Empty;
        private DateTime? _birthDate;
        private string _selectedGender = string.Empty;
        private bool _isBusy;

        // Список вариантов для выбора пола
        private ObservableCollection<string> _genderOptions = new()
        {
            "Мужской",
            "Женский",
            "Другое"
        };

        public RegPageViewModel(AuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            RegisterCommand = new AsyncRelayCommand(RegisterAsync, () => !IsBusy);
        }

        #region Properties

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string FootSize
        {
            get => _footSize;
            set => SetProperty(ref _footSize, value);
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        public string SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value);
        }

        public ObservableCollection<string> GenderOptions
        {
            get => _genderOptions;
            set => SetProperty(ref _genderOptions, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    RegisterCommand.NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Commands

        public IAsyncRelayCommand RegisterCommand { get; }

        #endregion

        #region Registration Logic

        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Проверка всех обязательных полей
                if (string.IsNullOrWhiteSpace(Name) ||
                    string.IsNullOrWhiteSpace(Login) ||
                    string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(FootSize) ||
                    BirthDate == null ||
                    string.IsNullOrWhiteSpace(SelectedGender))
                {

                    MessageBox.Show($"""
Name: {Name}
Login: {Login}
Email: {Email}
Password: {Password}
FootSize: {FootSize}
BirthDate: {BirthDate}
SelectedGender: {SelectedGender}
""");
                    MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка размера стопы
                if (!int.TryParse(FootSize, out int footSize) || footSize <= 0)
                {
                    MessageBox.Show("Введите корректный размер стопы (целое положительное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка существования пользователя
                if (await _authService.UserExistsAsync(Login, Email))
                {
                    MessageBox.Show("Пользователь с таким логином или email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание пользователя
                var user = new UserModel
                {
                    Name = Name,
                    Login = Login,
                    Email = Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(Password),
                    BirthDate = BirthDate ?? default,
                    Gender = SelectedGender,
                    FootSize = footSize
                };

                await _authService.RegisterAsync(user);

                MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очистка формы
                Name = string.Empty;
                Login = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                FootSize = string.Empty;
                BirthDate = null;
                SelectedGender = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}