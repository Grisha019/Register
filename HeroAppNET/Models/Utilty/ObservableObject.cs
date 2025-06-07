using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Models.Utilty
{
    public class ObservableObject : INotifyPropertyChanged
    {
        // Fix for CS8612: Make the event nullable to match the interface definition  
        public event PropertyChangedEventHandler? PropertyChanged;

        // Fix for CS8618: Initialize the event to null explicitly  
        protected virtual void OnPropertyChanged([CallerMemberName] string paramName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paramName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string paramName = "")
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(paramName);
            return true;
        }
    }
}
