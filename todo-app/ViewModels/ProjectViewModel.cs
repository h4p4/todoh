namespace todo_app.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    using todo_app.Core.Models;

    public partial class ProjectViewModel : ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TaskViewModel> _tasks;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _name;

        public ProjectViewModel()
        {
            _tasks = new ObservableCollection<TaskViewModel>();
            IsEditing = true;
            _tasks.CollectionChanged += SubTasksChanged;

        }

        public ProjectViewModel(Project project)
        {
            IsEditing = false;

            //TODO:
            // initialize tasks
            //_tasks.CollectionChanged += SubTasksChanged;
        }

        [ICommand]
        private void CreateTask(TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                _tasks.Add(
                    new TaskViewModel
                    {
                        Name = "test"
                    });
                return;
            }

            taskViewModel.SubTasks.Add(
                new TaskViewModel
                {
                    Name = "test_subtask"
                });
        }

        [ICommand]
        private void DeleteTask(TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
                return;
            taskViewModel.DeleteTask();
        }

        [ICommand]
        private void EditProject()
        {
            IsEditing = !IsEditing;
        }

        private void SubTaskDeleting(object? sender, EventArgs e)
        {
            var viewModel = (TaskViewModel)sender;
            _tasks.Remove(viewModel);
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
}