namespace todo_app.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using todo_app.Commands;

    public partial class ViewModel : INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
    {
        private bool _isEditing;

        public ViewModel()
        {
            EditCommand = new Command(Edit);
        }

        public ICommand EditCommand { get; }

        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return Enumerable.Empty<DataErrorsChangedEventArgs>();
        }

        public bool HasErrors { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;


        // ReSharper disable once VirtualMemberNeverOverridden.Global
        // ReSharper disable once UnusedMethodReturnValue.Global
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        private void Edit()
        {
            IsEditing = !IsEditing;
        }
    }
}