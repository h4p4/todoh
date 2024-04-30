namespace todo_app.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainViewModel : ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ProjectViewModel> _projects;

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
        private void RemoveProject(ProjectViewModel projectViewModel)
        {
            _projects.Remove(projectViewModel);
        }
    }
}