using HeroAppNET.Services.NavigationService;
using HeroAppNET.ViewModel.Base;
using HeroAppNET.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService LocalNavigationService
        {
            get => _navigationService;
            private set => Set(ref _navigationService, value); // Fix: Use SetProperty method from ViewModelBase  
        }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
