using HeroAppNET.Models.Utilty;
using HeroAppNET.ViewModel.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Services.NavigationService
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private NavigationService _instance;
        private IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /*public static NavigationService GetInstance()
        {
            if (_instance == null)
                _instance = new NavigationService();

            return _instance;
        }*/

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            private set => Set(ref _currentView, value);
        }

        public void NavigateTo<TViewModelBase>() where TViewModelBase : ViewModelBase
        {
            //ViewModelBase newViewModel = (ViewModelBase)Activator.CreateInstance(typeof(TViewModelBase));
            ViewModelBase newViewModel =
                (ViewModelBase)_serviceProvider.GetRequiredService(typeof(TViewModelBase));
            CurrentView = newViewModel;
        }//
    }
}
