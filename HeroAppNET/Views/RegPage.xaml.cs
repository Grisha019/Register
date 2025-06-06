using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HeroAppNET.Views
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        // Обработчик клика по гиперссылке "Войти"
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Навигация внутри приложения
            NavigationService navService = NavigationService.GetNavigationService(this);
            if (navService != null)
            {
                navService.Navigate(new Uri("/Views/LoginView.xaml", UriKind.Relative));
            }
            e.Handled = true;
        }
    }
}