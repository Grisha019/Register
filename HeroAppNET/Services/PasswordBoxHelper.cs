using System.Windows;
using System.Windows.Controls;

namespace HeroAppNET.Services
{
    public static class PasswordBoxHelper
    {
        // Привязываемое свойство для пароля
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

        // Режим, который позволяет отключить автоматическую синхронизацию (опционально)
        public static readonly DependencyProperty UpdateOnPasswordChanged =
            DependencyProperty.RegisterAttached(
                "UpdateOnPasswordChanged",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false));


        #region Методы доступа к Attached Properties

        public static string GetBoundPassword(DependencyObject dp) =>
            (string)dp.GetValue(BoundPassword);

        public static void SetBoundPassword(DependencyObject dp, string value) =>
            dp.SetValue(BoundPassword, value);

        public static bool GetUpdateOnPasswordChanged(DependencyObject dp) =>
            (bool)dp.GetValue(UpdateOnPasswordChanged);

        public static void SetUpdateOnPasswordChanged(DependencyObject dp, bool value) =>
            dp.SetValue(UpdateOnPasswordChanged, value);

        #endregion


        #region Обработчики событий

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                if (e.NewValue is string newPassword)
                {
                    passwordBox.Password = newPassword;
                }
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null && GetUpdateOnPasswordChanged(passwordBox))
            {
                SetBoundPassword(passwordBox, passwordBox.Password);
            }
        }

        #endregion
    }
}