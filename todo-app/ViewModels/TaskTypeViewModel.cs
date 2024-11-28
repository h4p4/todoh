namespace todo_app.ViewModels;

using System;
using System.Windows.Input;

using todo_app.Commands;
using todo_app.Core.Models;

public partial class TaskTypeViewModel : ViewModel
{
    private string _description;

    private string _name;

    public TaskTypeViewModel()
    {
    }

    public TaskTypeViewModel(TaskType taskType)
    {
        throw new NotImplementedException();
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }


}