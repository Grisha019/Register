using HeroAppNET.Views; // Подключаем пространство имён с LoginView
using HeroAppNET.ViewModel.Base;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Services.AuthenticationService;
using System;

public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly AuthService _authService;

    private string _loginField = string.Empty; // Инициализация
    public string LoginField
    {
        get => _loginField;
        set { _loginField = value; OnPropertyChanged(); }
    }

    private string _password = string.Empty; // Инициализация
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(ApplicationContext context)
    {
        _authService = new AuthService(context);
        LoginCommand = new RelayCommand(async () => await LoginAsync());
    }

    private async Task LoginAsync()
    {
        try
        {
            bool isAuthenticated = await _authService.AuthenticateAsync(LoginField, Password);
            if (!isAuthenticated)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Навигация
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Переопределяем ViewType, чтобы NavigationService знал, какую View отображать
    public override Type? ViewType => typeof(LoginView);

    // Fix for CS0506: Remove the override keyword as PropertyChanged in ObservableObject is not virtual, abstract, or override  
    public new event PropertyChangedEventHandler? PropertyChanged;
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName != null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
