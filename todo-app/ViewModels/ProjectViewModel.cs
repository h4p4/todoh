namespace todo_app.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using todo_app.Commands;
    using todo_app.Core.Models;

    public partial class ProjectViewModel : RecursiveTaskViewModel
    {
        private ObservableCollection<TaskStateViewModel> _defaultTaskStates;
        private ObservableCollection<TaskTypeViewModel> _defaultTaskTypes;
        private string _description;
        private string _name;
        private TaskViewModel _selectedTask;

        public ProjectViewModel()
        {
            CreateTaskCommand = new Command(CreateTask);
            DeleteTaskCommand = new Command((Action<object?>)DeleteTask);
            UpdateSelectedTaskCommand = new Command(UpdateSelectedTask);

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

            DefaultTaskStates = new ObservableCollection<TaskStateViewModel>
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


        public ICommand CreateTaskCommand { get; }

        public ICommand DeleteTaskCommand { get; }

        public ICommand UpdateSelectedTaskCommand { get; }

        public ObservableCollection<TaskStateViewModel> DefaultTaskStates { get; }

        public ObservableCollection<TaskTypeViewModel> DefaultTaskTypes { get; }

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

        public TaskViewModel SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }

        private void CreateTask(object? parameter)
        {
            var newTask = new TaskViewModel();

            if (parameter == null)
            {
                Tasks.Add(newTask);
                return;
            }

            if (parameter is not TaskViewModel taskViewModel)
                return;

            taskViewModel.Tasks.Add(newTask);
            SelectedTask = newTask;
        }

        private void DeleteTask(object? parameter)
        {
            if (parameter is not TaskViewModel taskViewModel)
                return;

            taskViewModel.DeleteTask();
        }

        private void UpdateSelectedTask(object? obj)
        {
            if (obj is not TaskViewModel taskViewModel)
                return;

            SelectedTask = taskViewModel;
        }
    }
}