namespace todo_app.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using todo_app.Commands;
    using todo_app.Core.Models;

    public partial class ProjectViewModel : RecursiveTaskViewModel
    {
        private string _description;
        private string _name;
        private TaskViewModel _selectedTask;

        public ProjectViewModel()
        {
            UpdateSelectedTaskCommand = new Command<TaskViewModel>(UpdateSelectedTask);

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

        private void UpdateSelectedTask(TaskViewModel? taskViewModel)
        {
            if (taskViewModel != null)
                SelectedTask = taskViewModel;
        }
    }
}