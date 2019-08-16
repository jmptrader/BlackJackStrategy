using System;

namespace MVVMShirt.Commands
{
    public class RelayCommand<T> : IRelayCommand
    {
        protected readonly Action<T> _execute;
        protected readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;            
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            return _canExecute((T)param);
        }

        public void Execute(object param)
        {
            _execute((T)param);
        }
    }
}
