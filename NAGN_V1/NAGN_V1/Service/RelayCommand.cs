using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NAGN_V1
{
    public class RelayCommand : ICommand
    { 
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public RelayCommand(Action execute,Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            else
                return _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
    public class RelayCommand<T> : ICommand
    {
        public readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute= canExecute;
        }
        

        public bool CanExecute(object? parameter)
        {
            if( _canExecute == null) return true;
            if(parameter == null && typeof(T).IsValueType) return false;
            return _canExecute((T?)parameter);
        }

        public void Execute(object? parameter)
        {
            _execute((T?)parameter);
        }
    }
}
