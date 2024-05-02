namespace todo_app.ViewModels
{
    using System.Collections.ObjectModel;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    using todo_app.Core.Models;

    public partial class ProjectViewModel : RecursiveTaskContainerViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TaskTypeViewModel> _defaultTaskTypes;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _name;

        public ProjectViewModel()
        {
            IsEditing = true;
            _defaultTaskTypes = new ObservableCollection<TaskTypeViewModel>
            {
                new()
                {
                    Name = "Раздел",
                },
                new()
                {
                    Name = "Задача",
                },
                new()
                {
                    Name = "Подзадача",
                },
            };
        }

        public ProjectViewModel(Project project)
        {
            IsEditing = false;

            //TODO:
            // initialize tasks
            //_tasks.CollectionChanged += SubTasksChanged;
        }

        [ICommand]
        private void CreateTask(TaskViewModel? taskViewModel)
        {
            var newTask = new TaskViewModel();

            if (taskViewModel == null)
            {
                Tasks.Add(newTask);
                return;
            }

            taskViewModel.Tasks.Add(newTask);
        }

        [ICommand]
        private void DeleteTask(TaskViewModel? taskViewModel)
        {
            taskViewModel?.DeleteTask();
        }
    }
}