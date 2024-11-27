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
        AddSubTaskCommand = new Command(AddSubTask);
    }

    public TaskTypeViewModel(TaskType taskType)
    {
        throw new NotImplementedException();
    }

    public ICommand AddSubTaskCommand { get; }

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

    private void AddSubTask(object? parameter)
    {
        if (parameter is not RecursiveTaskViewModel taskViewModel)
            return;
        var newTask = new TaskViewModel { Type = this };
        taskViewModel.Tasks.Add(newTask);
    }
}