/* Todo */

namespace TodoListProject
{
    public class Todo
    {
        public int Id { get; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Project ParentProject { get; set; }

        public Todo(string title, DateTime dueDate, Project project)
        {
            Id = new Random().Next(1, 100000); // Simple random ID generation
            Title = title;
            DueDate = dueDate;
            IsCompleted = false;
            ParentProject = project;
        }

        // Mark the todo as completed
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        // Edit the todo details
        public void EditTodo(
            string newTitle = null, 
            DateTime? newDueDate = null,
            bool? isCompleted = null,
            Project newProject = null)
        {
            // Update only the provided fields
            Title = newTitle ?? Title;
            DueDate = newDueDate ?? DueDate;
            IsCompleted = isCompleted ?? IsCompleted;
            ParentProject = newProject ?? ParentProject;
        }
    }
}
