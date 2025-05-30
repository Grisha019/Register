using HeroAppNET.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Services.NavigationService
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModelBase>() where TViewModelBase : ViewModelBase;
    }
}
