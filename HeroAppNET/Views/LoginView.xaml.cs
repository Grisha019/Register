// LoginView.xaml.cs

using System.Windows;
using System.Windows.Controls;

namespace HeroAppNET.Views
{
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно реализовать логику авторизации
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Пример: показать сообщение
            MessageBox.Show($"Логин: {login}\nПароль: {password}", "Вход");
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
