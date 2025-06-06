using HeroAppNET.Services.AuthenticationService;
using HeroAppNET.ViewModel.Base;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

public class LoginPageViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly AuthService _authService = new AuthService();

    private string _loginField;
    public string LoginField
    {
        get => _loginField;
        set { _loginField = value; OnPropertyChanged(); }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    public LoginPageViewModel()
    {
        LoginCommand = new RelayCommand(Login);
    }

    private void Login()
    {
        if (_authService.Authenticate(LoginField, Password))
        {
            MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Перейти на главную страницу приложения  
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
