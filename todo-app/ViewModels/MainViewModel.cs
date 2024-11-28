namespace todo_app.ViewModels
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using todo_app.Commands;

    public partial class MainViewModel : ViewModel
    {
        private ObservableCollection<ProjectViewModel> _selectedProjects;

        private ProjectViewModel _selectedProject;

        public MainViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();

            AddProjectCommand = new Command(AddProject);
            EditProjectsCommand = new Command<IList>(EditProjects);
            RemoveProjectsCommand = new Command<IList>(RemoveProjects);
        }

        public ICommand AddProjectCommand { get; }

        public ICommand EditProjectsCommand { get; }

        public ICommand RemoveProjectsCommand { get; }

        public ObservableCollection<ProjectViewModel> Projects { get; }

        public ObservableCollection<ProjectViewModel> SelectedProjects
        {
            get => _selectedProjects;
            set => SetProperty(ref _selectedProjects, value);
        }

        public ProjectViewModel SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }

        private void AddProject()
        {
            var newProject = new ProjectViewModel();
            Projects.Add(newProject);
            SelectedProject = newProject;
        }

        private void EditProjects(IList? projectViewModels)
        {
            if (projectViewModels is null)
                return;

            var viewModels = projectViewModels.Cast<ProjectViewModel>().ToList();
            var isEditing = SelectedProject.IsEditing;
            viewModels.ForEach(model => model.IsEditing = !isEditing);
        }

        private void RemoveProjects(IList? projectViewModels)
        {
            var viewModels = projectViewModels?.Cast<ProjectViewModel>().ToList();
            viewModels?.ForEach(model => Projects.Remove(model));
        }
    }
}