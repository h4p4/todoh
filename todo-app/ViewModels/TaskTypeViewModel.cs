namespace todo_app.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class TaskTypeViewModel : ViewModel
{
    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _name;
}