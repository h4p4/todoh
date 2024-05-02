namespace todo_app.ViewModels
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainViewModel : ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ProjectViewModel> _projects;

        [ObservableProperty]
        private ObservableCollection<ProjectViewModel> _selectedProjects;

        private ProjectViewModel _selectedProject;

        public MainViewModel()
        {
            _projects = new ObservableCollection<ProjectViewModel>();
        }

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }

        [ICommand]
        private void AddProject()
        {
            _projects.Add(new ProjectViewModel());
        }

        [ICommand]
        private void EditProjects(object projectViewModels)
        {
            if (projectViewModels is not IList list)
                return;

            var viewModels = list.Cast<ProjectViewModel>().ToList();
            var isEditing = SelectedProject.IsEditing;
            viewModels.ForEach(model => model.IsEditing = !isEditing);
        }

        [ICommand]
        private void RemoveProjects(object projectViewModels)
        {
            if (projectViewModels is not IList list)
                return;

            var viewModels = list.Cast<ProjectViewModel>().ToList();
            viewModels.ForEach(model => _projects.Remove(model));
        }
    }
}