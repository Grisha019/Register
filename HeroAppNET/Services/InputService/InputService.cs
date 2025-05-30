using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HeroAppNET.Services.InputService
{
    public class InputService : IInputService
    {
        private Window _mainWindow;
        private readonly HashSet<Key> _pressedKeys = new HashSet<Key>();
        public InputService(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

            _mainWindow.PreviewKeyDown += PreviewKeyDown;
            _mainWindow.PreviewKeyUp += PreviewKeyUp;

            _mainWindow.LostFocus += _mainWindow_LostFocus;
        }

        ~InputService()
        {
            _mainWindow.PreviewKeyDown -= PreviewKeyDown;
            _mainWindow.PreviewKeyUp -= PreviewKeyUp;
            _mainWindow.LostFocus -= _mainWindow_LostFocus;
        }

        public bool IsKeyPressed(Key key)
        {
            return _pressedKeys.Contains(key);
        }
        public IEnumerable<Key> PressedKeys => _pressedKeys;

        private void _mainWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            _pressedKeys.Clear();
        }

        private void PreviewKeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.Key);
        }

        private void PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _pressedKeys.Add(e.Key);
        }
    }
}
