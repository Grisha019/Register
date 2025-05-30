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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string paramName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(paramName));
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
