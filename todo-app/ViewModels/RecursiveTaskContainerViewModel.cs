namespace todo_app.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public class RecursiveTaskContainerViewModel : ViewModel
    {
        private ObservableCollection<RecursiveTaskContainerViewModel> _tasks;

        public RecursiveTaskContainerViewModel()
        {
            Tasks = new ObservableCollection<RecursiveTaskContainerViewModel>();
            Tasks.CollectionChanged += SubTasksChanged;
        }

        public IEnumerable<RecursiveTaskContainerViewModel> AllNestedTasks =>
            Tasks.SelectMany(x => x.AllNestedTasks).Concat(Tasks);


        public ObservableCollection<RecursiveTaskContainerViewModel> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public void DeleteTask()
        {
            Deleting.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Deleting;

        private void SubTaskDeleting(object? sender, EventArgs e)
        {
            var viewModel = (RecursiveTaskContainerViewModel)sender;
            Tasks.Remove(viewModel);
        }

        private void SubTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
                return;

            foreach (RecursiveTaskContainerViewModel subTask in e.NewItems)
            {
                subTask.Deleting += SubTaskDeleting;
            }
        }
    }
}