namespace todo_app.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class ViewModel : ObservableObject
    {
        private bool _isEditing;

        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        [ICommand]
        private void Edit()
        {
            IsEditing = !IsEditing;
        }
    }
}