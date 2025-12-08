/* Todo */

namespace TodoList
{
    public class Todo
    {
        public int Id { get; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Project Project { get; set; }

        public Todo(string title, DateTime dueDate, Project project)
        {
            Id = new Random().Next(1, 100000); // Simple random ID generation
            Title = title;
            DueDate = dueDate;
            IsCompleted = false;
            Project = project;
        }

        // Mark the todo as completed
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        // Edit the todo details
        public void Edit(
            string newTitle = null, 
            DateTime? newDueDate = null,
            bool? isCompleted = null,
            Project newProject = null)
        {
            // Update only the provided fields
            Title = newTitle ?? Title;
            DueDate = newDueDate ?? DueDate;
            IsCompleted = isCompleted ?? IsCompleted;
            Project = newProject ?? Project;
        }
    }
}
