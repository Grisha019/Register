using HeroAppNET.Infrastructure.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroAppNET.Infrastructure.Command
{
    public class RelayCommand : BaseCommand
    {
        private Action<object?> _action;
        private Func<object?, bool>? _canExecute; // Allow nullable reference type  

        public RelayCommand(Action<object?> action, Func<object?, bool>? canExecute = null) // Add nullable annotation  
        {
            _action = action;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) // Adjust nullable annotation to match the base class  
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public override void Execute(object? parameter) // Adjust nullable annotation to match the base class  
        {
            _action(parameter);
        }
    }
}
