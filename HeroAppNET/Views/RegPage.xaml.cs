using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using HeroAppNET.Services.AuthenticationService;
using System.Windows.Navigation;
using System.Diagnostics;

namespace HeroAppNET.Views
{
    public partial class RegPage : Page
    {
        // Конструктор через DI
        public RegPage(RegPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        // Конструктор для поддержки дизайнера
        public RegPage() : this(new RegPageViewModel(CreateAuthService()))
        {
        }

        // Метод для создания AuthService с контекстом
        private static AuthService CreateAuthService()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite("Data Source=app.db")
                .Options;

            var context = new ApplicationContext(options);
            return new AuthService(context);
        }

        // Обработчик перехода по гиперссылке
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Навигация через NavigationService
            App.NavigationService?.NavigateTo<LoginViewModel>();
            e.Handled = true;
        }
    }
}
