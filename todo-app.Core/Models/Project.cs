namespace todo_app.Core.Models
{
    using System.Collections.Generic;

    public class Project
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }
}