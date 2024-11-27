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
            EditProjectsCommand = new Command(EditProjects);
            RemoveProjectsCommand = new Command(RemoveProjects);
            //SelectedProjectChangedCommand = new Command(OnSelectedProjectChanged);
        }

        public ICommand AddProjectCommand { get; }

        public ICommand EditProjectsCommand { get; }

        public ICommand RemoveProjectsCommand { get; }

        public ObservableCollection<ProjectViewModel> Projects { get; }

        //public ICommand SelectedProjectChangedCommand { get; }

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

        private void EditProjects(object? projectViewModels)
        {
            if (projectViewModels is not IList list)
                return;

            var viewModels = list.Cast<ProjectViewModel>().ToList();
            var isEditing = SelectedProject.IsEditing;
            viewModels.ForEach(model => model.IsEditing = !isEditing);
        }

        //private void OnSelectedProjectChanged(object? obj)
        //{
        //    if (obj is not ProjectViewModel projectViewModel)
        //        return;

        //    SelectedProject = projectViewModel;
        //}

        private void RemoveProjects(object? projectViewModels)
        {
            if (projectViewModels is not IList list)
                return;

            var viewModels = list.Cast<ProjectViewModel>().ToList();
            viewModels.ForEach(model => Projects.Remove(model));
        }
    }
}