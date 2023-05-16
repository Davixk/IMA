using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClientCS.Commands
{
    public class RelayCommandAsync : ICommand
    {
        private readonly Func<Task> _esegui;
        private readonly Predicate<object> _puoEseguire;
        private bool staEseguendo;

        public RelayCommandAsync(Func<Task> esegui) : this(esegui, null) { }

        public RelayCommandAsync(Func<Task> esegui, Predicate<object> canExecute)
        {
            _esegui = esegui;
            _puoEseguire = canExecute;
        }

        public bool CanExecute(object parametro)
        {
            if (!staEseguendo && _puoEseguire == null) return true;
            return (!staEseguendo && _puoEseguire(parametro));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            staEseguendo = true;
            try { await _esegui(); }
            finally { staEseguendo = false; }
        }
    }
}
