using HeroAppNET.Infrastructure.ApplicationContext;
using HeroAppNET.Models.Utilty;
using HeroAppNET.Services.InputService;
using HeroAppNET.Services.NavigationService;
using HeroAppNET.ViewModel;
using HeroAppNET.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HeroAppNET
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Регистрация DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("Host=localhost;Port=5432;Database=WPF;Username=postgres;Password=123");


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=WPF;Username=postgres;Password=123");
            });

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<RegPageViewModel>();




           


            services.AddTransient<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });

            services.AddTransient<RegPage>(provider => new RegPage()
            {
                DataContext = provider.GetRequiredService<RegPageViewModel>()
            });


            services.AddScoped<INavigationService, NavigationService>(provider =>
                new NavigationService(provider));

            services.AddSingleton<IInputService, InputService>();

            _serviceProvider = services.BuildServiceProvider();
            var activator = ActivatorDI.GetInstance();
            activator.SetServiceProvider(_serviceProvider);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWin = new MainWindow(); 
            var regPage = _serviceProvider.GetRequiredService<RegPage>(); 
            mainWin.Content = regPage;
            mainWin.Show();

            base.OnStartup(e);
        }
    }

}
