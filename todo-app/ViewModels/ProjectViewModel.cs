namespace todo_app.ViewModels
{
    using System.Collections.ObjectModel;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    using todo_app.Core.Models;

    public partial class ProjectViewModel : RecursiveTaskContainerViewModel
    {
        private ObservableCollection<TaskStateViewModel> _defaultTaskStates;
        private ObservableCollection<TaskTypeViewModel> _defaultTaskTypes;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _name;

        public ProjectViewModel()
        {
            IsEditing = true;
            DefaultTaskTypes = new ObservableCollection<TaskTypeViewModel>
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
                new()
                {
                    Name = "Ошибка",
                },
            };

            DefaultTaskStates = new ObservableCollection<TaskStateViewModel>()
            {
                new()
                {
                    Name = "Пул"
                },
                new()
                {
                    Name = "В работе"
                },
                new()
                {
                    Name = "Завершено"
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

        public ObservableCollection<TaskStateViewModel> DefaultTaskStates
        {
            get => _defaultTaskStates;
            set => SetProperty(ref _defaultTaskStates, value);
        }

        public ObservableCollection<TaskTypeViewModel> DefaultTaskTypes
        {
            get => _defaultTaskTypes;
            set => SetProperty(ref _defaultTaskTypes, value);
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