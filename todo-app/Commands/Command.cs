namespace todo_app.Commands
{
    using System;
    using System.Windows.Input;

    public delegate bool BooleanAction();

    public class Command : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Command(Action execute, BooleanAction? canExecute = null)
        {
            _execute = _ => execute();
            if (canExecute == null)
                return;

            _canExecute = _ => canExecute();
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) != false;
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}