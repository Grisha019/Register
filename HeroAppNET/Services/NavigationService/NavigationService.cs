using CommunityToolkit.Mvvm.ComponentModel;
using HeroAppNET.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace HeroAppNET.Services.NavigationService
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ViewModelBase? _currentView;

        public Frame? Frame { get; set; }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ViewModelBase? CurrentView
        {
            get => _currentView;
            private set => SetProperty(ref _currentView, value);
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            if (CurrentView?.GetType() == typeof(TViewModel))
                return;

            if (viewModel.ViewType == null)
                throw new InvalidOperationException($"ViewType не определён для {typeof(TViewModel).Name}");

            var view = (Page)_serviceProvider.GetRequiredService(viewModel.ViewType);
            view.DataContext = viewModel;

            CurrentView?.OnNavigatedFrom();
            CurrentView = viewModel;
            viewModel.OnNavigatedTo(null);

            if (Frame == null)
                throw new InvalidOperationException("Frame не установлен для NavigationService");

            Frame.Navigate(view);
        }

        public void Navigate<TPage>() where TPage : Page
        {
            if (Frame == null)
                throw new InvalidOperationException("Frame не установлен для NavigationService");

            var page = _serviceProvider.GetRequiredService<TPage>();
            Frame.Navigate(page);
        }
    }
}
