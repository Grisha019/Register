using HeroAppNET.ViewModel.Base;

namespace HeroAppNET.Services.NavigationService
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    }
}