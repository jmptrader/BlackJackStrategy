using System;
using System.Threading.Tasks;

namespace MVVMShirt.Commands
{
    public class RelayCommandAsync<T> : IRelayCommandAsync
    {
        protected readonly Func<T, Task> _execute;
        protected readonly Predicate<T> _canExecute;

        public RelayCommandAsync(Func<T, Task> execute)
        {
            _execute = execute;            
        }

        public RelayCommandAsync(Func<T, Task> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object param)
        {
            return _canExecute((T)param);
        }

        public async Task Execute(object param)
        {
            await _execute((T)param);
        }
    }
}
