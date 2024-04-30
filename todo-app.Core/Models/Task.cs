namespace todo_app.Core.Models
{
    public class Task
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Task> SubTasks { get; set; }
        public TaskState TaskState { get; set; }
        public TaskType Type { get; set; }
    }
}