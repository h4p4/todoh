namespace todo_app.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

using todo_app.Core.Models;

public partial class TaskViewModel : RecursiveTaskContainerViewModel
{
    [ObservableProperty]
    private string _description;

    private string _name;
    private TaskStateViewModel _state;
    private TaskTypeViewModel _type;

    public TaskViewModel()
    {
    }

    public TaskViewModel(Task task)
    {
        //Type = new TaskTypeViewModel(task.Type);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public TaskStateViewModel State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }

    public TaskTypeViewModel Type
    {
        get => _type;
        set => SetProperty(ref _type, value);
    }
}