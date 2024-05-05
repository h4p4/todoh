namespace todo_app.ViewModels;

using System;

using todo_app.Core.Models;

public class TaskStateViewModel : ViewModel
{
    private string _name;

    public TaskStateViewModel()
    {
    }

    public TaskStateViewModel(TaskState taskState)
    {
        // TODO:
        throw new NotImplementedException();
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}