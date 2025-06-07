using HeroAppNET.Services.AuthenticationService;
using HeroAppNET.Infrastructure.ApplicationContext;
using System.Windows;
using System.Windows.Controls;

namespace HeroAppNET.Views
{
    public partial class LoginView : Page
    {
        private readonly AuthService _authService;

        public LoginView()
        {
            InitializeComponent();
            var context = new ApplicationContext(); // Тут нужно получить твой контекст из DI или создать
            _authService = new AuthService(context);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            bool userExists = await _authService.UserExistsAsync(login);
            if (!userExists)
            {
                MessageBox.Show("Пользователь с таким логином не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool passwordCorrect = await _authService.CheckPasswordAsync(login, password);
            if (!passwordCorrect)
            {
                MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            // Здесь переход на главную страницу или другая логика
        }

        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
                LoginPlaceholder.Visibility = Visibility.Visible;
        }
    }
}
