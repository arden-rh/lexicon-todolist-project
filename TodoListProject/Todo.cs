/* Todo */

namespace TodoListProject
{
    public class Todo
    {
        public int Id { get; }
        public string Title { get; set; }
        public DateOnly DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string ParentProjectName { get; set; }

        // Constructor for creating a new Todo
        public Todo(string title, DateOnly dueDate, string projectName)
        {
            Id = new Random().Next(1, 100); // Simple random ID generation
            Title = title;
            DueDate = dueDate;
            IsCompleted = false;
            ParentProjectName = projectName;
        }

        // Constructor for creating a Todo with all properties
        internal Todo(int id, string title, DateOnly dueDate, bool isCompleted, string projectName)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            IsCompleted = isCompleted;
            ParentProjectName = projectName;
        }

        // Mapping method to create a Todo from data record
        public static Todo FromRecord(TodoRecord record)
        {
            return new Todo(
                record.Id,
                record.Title,
                record.DueDate,
                record.IsCompleted,
                record.ParentProjectName
            );
        }

        // Mapping method to convert a Todo to data record
        public TodoRecord ToRecord()
        {
            return new TodoRecord(
                Id,
                Title,
                DueDate,
                IsCompleted,
                ParentProjectName
            );
        }

        // Mark the todo as completed
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        // Edit the todo details
        public void EditTodo(
            string newTitle = null, 
            DateOnly? newDueDate = null,
            bool? isCompleted = null,
            string newProjectName = null)
        {
            // Update only the provided fields
            Title = newTitle ?? Title;
            DueDate = newDueDate ?? DueDate;
            IsCompleted = isCompleted ?? IsCompleted;
            ParentProjectName = newProjectName ?? ParentProjectName;
        }
    }
}
