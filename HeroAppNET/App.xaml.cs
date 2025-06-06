using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models.Utilty; // Исправлено: было "Utilty"
using HeroAppNET.Services.InputService;
using HeroAppNET.Services.NavigationService;
using HeroAppNET.ViewModel;
using HeroAppNET.Views;
using HeroAppNET.Services.AuthenticationService;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using HeroAppNET;
using Microsoft.EntityFrameworkCore;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();

        // Регистрация контекста базы данных (PostgreSQL)
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=WPF;Username=postgres;Password=123"));

        // Регистрация сервисов
        services.AddScoped<AuthService>(); // Сервис аутентификации

        // Регистрация ViewModel'ей
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<RegPageViewModel>();

        // Регистрация окон (Window)
        services.AddTransient<MainWindow>();
        services.AddTransient<RegPage>();

        // Регистрация NavigationService
        services.AddScoped<INavigationService, NavigationService>(provider =>
            new NavigationService(provider));

        // Регистрация InputService
        services.AddSingleton<IInputService, InputService>();

        // Сборка провайдера зависимостей
        _serviceProvider = services.BuildServiceProvider();

        // Передача сервиса DI в статический активатор, если используется где-то еще
        var activator = ActivatorDI.GetInstance();
        activator.SetServiceProvider(_serviceProvider);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Запуск главного окна и отображение страницы регистрации как содержимого
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        var regPage = _serviceProvider.GetRequiredService<RegPage>();

        mainWindow.Content = regPage;
        mainWindow.Show();
    }
}