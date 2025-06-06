using CommunityToolkit.Mvvm.ComponentModel;
using HeroAppNET.ViewModel.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeroAppNET.Services.NavigationService
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ViewModelBase _currentView;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ViewModelBase CurrentView
        {
            get => _currentView;
            private set => SetProperty(ref _currentView, value);
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            if (CurrentView?.GetType() == typeof(TViewModel))
                return;

            CurrentView = viewModel;

            var mainWindow = Application.Current.MainWindow as FrameworkElement; // Change type to FrameworkElement  
            if (mainWindow != null)
            {
                mainWindow.DataContext = CurrentView; // FrameworkElement supports DataContext  
            }
            else
            {
                var frame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
                if (frame != null)
                {
                    var viewType = viewModel.ViewType;
                    if (viewType != null)
                    {
                        var view = (UIElement)_serviceProvider.GetRequiredService(viewType);
                        frame.Content = view;
                    }
                }
            }
        }
    }
}