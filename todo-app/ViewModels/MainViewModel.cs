namespace todo_app.ViewModels
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainViewModel : ViewModel
    {
        private ObservableCollection<ProjectViewModel> _projects;

        [ObservableProperty]
        private ObservableCollection<ProjectViewModel> _selectedProjects;

        private ProjectViewModel _selectedProject;

        public MainViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
        }
        public ObservableCollection<ProjectViewModel> Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }

        [ICommand]
        private void AddProject()
        {
            var newProject = new ProjectViewModel();
            Projects.Add(newProject);
            SelectedProject = newProject;
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
            viewModels.ForEach(model => Projects.Remove(model));
        }
    }
}