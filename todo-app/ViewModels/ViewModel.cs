namespace todo_app.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    public abstract class ViewModel : ObservableObject
    {
        private bool _isEditing;

        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }
    }
}