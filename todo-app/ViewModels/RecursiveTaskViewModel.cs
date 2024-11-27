namespace todo_app.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;

    public class RecursiveTaskViewModel : ViewModel
    {
        public RecursiveTaskViewModel()
        {
            Tasks = new ObservableCollection<RecursiveTaskViewModel>();
            Tasks.CollectionChanged += OnSubTasksChanged;
        }

        public IEnumerable<RecursiveTaskViewModel> AllNestedTasks =>
            Tasks.SelectMany(x => x.AllNestedTasks).Concat(Tasks);

        public ObservableCollection<RecursiveTaskViewModel> Tasks { get; }

        public event EventHandler<EventArgs> Deleting;

        public void DeleteTask()
        {
            Deleting.Invoke(this, EventArgs.Empty);
        }

        private void OnSubTaskDeleting(object? sender, EventArgs e)
        {
            var viewModel = (RecursiveTaskViewModel)sender;
            Tasks.Remove(viewModel);
        }

        private void OnSubTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
                return;

            foreach (RecursiveTaskViewModel subTask in e.NewItems)
            {
                subTask.Deleting += OnSubTaskDeleting;
            }
        }
    }
}