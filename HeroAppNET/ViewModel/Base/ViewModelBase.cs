using HeroAppNET.Infrastructure.Command;
using HeroAppNET.Models.Utilty;
using HeroAppNET.Services.NavigationService;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HeroAppNET.ViewModel.Base
{
    public abstract class ViewModelBase : ObservableObject, IDisposable
    {
        #region Свойства

        private bool _isBusy;
        /// <summary>
        /// Указывает, занят ли ViewModel выполнением длительной операции.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        #endregion

        #region Навигация

        /// <summary>
        /// Тип представления, связанного с этой ViewModel.
        /// </summary>
        public virtual Type? ViewType => null;

        /// <summary>
        /// Вызывается при переходе к данной ViewModel.
        /// </summary>
        public virtual void OnNavigatedTo(object? parameter) { }

        /// <summary>
        /// Вызывается при выходе из ViewModel.
        /// </summary>
        public virtual void OnNavigatedFrom() { }

        #endregion

        #region Команды

        /// <summary>
        /// Базовая команда, которую можно переопределять в наследниках.
        /// </summary>
        public ICommand DefaultCommand => new RelayCommand(OnDefaultCommand);

        protected virtual void OnDefaultCommand(object? parameter)
        {
            // Можно оставить пустым или реализовать логику по умолчанию
        }

        #endregion

        #region IDisposable

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Освободить управляемые ресурсы
                }

                // Освободить неуправляемые ресурсы

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}