namespace todo_app.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class TaskViewModel : ViewModel
{
    private ObservableCollection<TaskViewModel> _subTasks;

    [ObservableProperty]
    private string _description;

    private string _name;

    [ObservableProperty]
    private TaskTypeViewModel _type;

    public TaskViewModel()
    {
        SubTasks = new ObservableCollection<TaskViewModel>();
        _type = new TaskTypeViewModel();
        SubTasks.CollectionChanged += SubTasksChanged;
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public ObservableCollection<TaskViewModel> SubTasks
    {
        get => _subTasks;
        set => SetProperty(ref _subTasks, value);
    }

    public void DeleteTask()
    {
        Deleting.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler<EventArgs> Deleting;

    private void SubTaskDeleting(object? sender, EventArgs e)
    {
        var viewModel = (TaskViewModel)sender;
        SubTasks.Remove(viewModel);
    }

    private void SubTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add)
            return;

        foreach (TaskViewModel subTask in e.NewItems)
        {
            subTask.Deleting += SubTaskDeleting;
        }
    }
}