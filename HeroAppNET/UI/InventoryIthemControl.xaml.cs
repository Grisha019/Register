using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeroAppNET.UI
{
    /// <summary>
    /// Логика взаимодействия для InventoryIthemControl.xaml
    /// </summary>
    public partial class InventoryIthemControl : UserControl
    {
        
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(
                nameof(ClickCommand), //Название передоваемого совойства
                typeof(ICommand), // Что мы хотим получить в control
                typeof(InventoryIthemControl), // в какой контрол он будет передаваться.
                new PropertyMetadata(
                    null, //Значение по умолчанию.
                    OnClickCommandChanged //Обработчик события о том что поменялся InvetoryItem.
                    )
                );



        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

    




        private static void OnClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as InventoryIthemControl;
            if (control != null)
            {
                control.ClickCommand = (ICommand)e.NewValue;
            }
        }
    }
}
