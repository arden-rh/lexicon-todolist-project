/* Todo */

namespace TodoList
{
    public class Todo
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public Project Project { get; set; }
    }
}
