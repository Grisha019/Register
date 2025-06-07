using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models;
using HeroAppNET.Models.Utilty;
using HeroAppNET.Services.AuthenticationService;
using HeroAppNET.Services.InputService;
using HeroAppNET.Services.NavigationService;
using HeroAppNET.ViewModel;
using HeroAppNET.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeroAppNET
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public static INavigationService? NavigationService { get; private set; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Регистрация контекста базы данных (SQLite)
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite("Data Source=app.db"));

            // Регистрация сервисов
            services.AddScoped<AuthService>();
            services.AddSingleton<IInputService, InputService>();

            // Регистрация ViewModel'ей
            services.AddTransient<RegPageViewModel>();
            services.AddTransient<LoginViewModel>();

            // Регистрация страниц
            services.AddTransient<RegPage>(provider =>
                new RegPage(provider.GetRequiredService<RegPageViewModel>()));

            // Регистрация NavigationService
            services.AddScoped<INavigationService, NavigationService>(provider =>
                new NavigationService(provider));


            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginView>();





            // Сборка провайдера зависимостей
            _serviceProvider = services.BuildServiceProvider();

            // Инициализация БД
            InitializeDatabase(_serviceProvider);

            // Передача DI в статический активатор (если используется)
            var activator = ActivatorDI.GetInstance();
            activator.SetServiceProvider(_serviceProvider);

            // Получение NavigationService
            NavigationService = _serviceProvider.GetService<INavigationService>();

            if (NavigationService == null)
                throw new InvalidOperationException("NavigationService не зарегистрирован");
        }

        private void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            try
            {
                context.Database.Migrate();
                Console.WriteLine("✅ База данных успешно обновлена");

                if (!context.Users.Any())
                {
                    context.Users.Add(new UserModel
                    {
                        Name = "Admin",
                        Login = "admin",
                        Email = "admin@example.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin123")
                    });

                    context.SaveChanges();
                    Console.WriteLine("✅ Тестовый пользователь добавлен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при инициализации БД: {ex.Message}");
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем главное окно вручную
            var mainWindow = new Window
            {
                Title = "HeroApp",
                Width = 800,
                Height = 600
            };

            // Создаем Frame для навигации
            var frame = new Frame
            {
                NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden
            };

            // Помещаем Frame в окно
            mainWindow.Content = frame;

            // Настраиваем навигационный сервис
            if (NavigationService is NavigationService navService)
            {
                navService.Frame = frame;
                navService.Navigate<RegPage>();
            }

            // Показываем окно
            mainWindow.Show();
        }
    }
}
