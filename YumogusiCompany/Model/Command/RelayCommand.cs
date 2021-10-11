using System;
using System.Windows.Input;

namespace YumogusiCompany.Model.Command
{
    internal class RelayCommand : ICommand
    {
        protected Action<object>? _execute;
        protected Func<object, bool>? _canExecute;

        protected RelayCommand() { }

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
