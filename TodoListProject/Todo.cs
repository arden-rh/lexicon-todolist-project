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
        public Todo(int id, string title, DateOnly dueDate, string projectName)
        {
            this.Id = id;
            this.Title = title;
            this.DueDate = dueDate;
            this.IsCompleted = false;
            ParentProjectName = projectName;
        }

        // Constructor for creating a Todo with all properties
        internal Todo(int id, string title, DateOnly dueDate, bool isCompleted, string projectName)
        {
            this.Id = id;
            this.Title = title;
            this.DueDate = dueDate;
            this.IsCompleted = isCompleted;
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
            string NewTitle = null, 
            DateOnly? NewDueDate = null,
            bool? NewIsCompleted = null,
            string NewProjectName = null)
        {
            // Update only the provided fields
            Title = NewTitle ?? Title;
            DueDate = NewDueDate ?? DueDate;
            IsCompleted = NewIsCompleted ?? IsCompleted;
            ParentProjectName = NewProjectName ?? ParentProjectName;
        }
    }
}
