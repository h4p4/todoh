namespace todo_app.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using todo_app.Commands;

    public class RecursiveTaskViewModel : ViewModel
    {
        private RecursiveTaskViewModel? _parentTask;

        public RecursiveTaskViewModel()
        {
            Tasks = new ObservableCollection<RecursiveTaskViewModel>();
            CreateInnerTaskCommand = new Command<TaskTypeViewModel>(CreateInnerTask);
            DeleteTaskCommand = new Command(DeleteTask);
            //Tasks.CollectionChanged += OnInnerTasksChanged;
        }

        public ICommand CreateInnerTaskCommand { get; }

        public ICommand DeleteTaskCommand { get; }

        public IEnumerable<RecursiveTaskViewModel> AllNestedTasks =>
            Tasks.SelectMany(x => x.AllNestedTasks).Concat(Tasks);

        public ObservableCollection<RecursiveTaskViewModel> Tasks { get; }

        //public event EventHandler<EventArgs> DeletionRequested;

        //public void RequestDeletion()
        //{
        //    DeletionRequested.Invoke(this, EventArgs.Empty);
        //}

        public RecursiveTaskViewModel? ParentTask
        {
            get => _parentTask;
            set => SetProperty(ref _parentTask, value);
        }

        private void CreateInnerTask(TaskTypeViewModel? taskType)
        {
            if (taskType == null)
                return;

            var newTask = new TaskViewModel { Type = taskType, ParentTask = this };
            Tasks.Add(newTask);
        }

        private void DeleteTask()
        {
            ParentTask?.Tasks.Remove(this);
            //taskViewModel?.RequestDeletion();
        }

        //private void OnInnerTaskDeletionRequested(object? sender, EventArgs e)
        //{
        //    if (sender is not RecursiveTaskViewModel recursiveTaskViewModel)
        //        return;

        //    recursiveTaskViewModel.DeletionRequested -= OnInnerTaskDeletionRequested;
        //    Tasks.Remove(recursiveTaskViewModel);
        //}

        //private void OnInnerTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.Action != NotifyCollectionChangedAction.Add)
        //        return;

        //    if (e.NewItems == null)
        //        return;

        //    foreach (RecursiveTaskViewModel subTask in e.NewItems)
        //    {
        //        subTask.DeletionRequested += OnInnerTaskDeletionRequested;
        //    }
        //}
    }
}