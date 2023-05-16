using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ChatClientCS.Commands
{
    /*SPIEGA ICOMMANDS ECCCCC
     * 
     * 
     * 
     * 
     * 
     * 
     * */
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _esegui;
        private readonly Predicate<object> _puoEseguire;

        public RelayCommand(Action<object> esegui) : this(esegui, null) { }

        public RelayCommand(Action<object> esegui, Predicate<object> puoEseguire)
        {
            if (esegui == null) throw new ArgumentNullException("esegui");
            _esegui = esegui;
            _puoEseguire = puoEseguire;
        }

        public bool CanExecute(object parametro)
        {
            return _puoEseguire == null ? true : _puoEseguire(parametro);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parametro)
        {
            _esegui(parametro);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _esegui;
        private readonly Predicate<T> _puoEseguire;

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public RelayCommand(Action<T> esegui, Predicate<T> canExecute)
        {
            if (esegui == null) throw new ArgumentNullException("esegui");
            _esegui = esegui;
            _puoEseguire = canExecute;
        }

        [DebuggerStepThrough()]
        public bool CanExecute(object parameter)
        {
            return _puoEseguire == null ? true : _puoEseguire((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _esegui((T)parameter);
        }
    }
}
