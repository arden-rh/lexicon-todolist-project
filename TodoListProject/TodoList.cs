/* Todo List */

namespace TodoListProject
{
    public class TodoList
    {
        private List<Todo> _Todos = new List<Todo>();

        // Get all Todo items
        public List<Todo> GetAllTodos()
        {
            return _Todos;
        }

        // Get all incomplete Todo items
        public List<Todo> GetIncompleteTodos()
        {
            return _Todos.Where(todo => !todo.IsCompleted).ToList();
        }

        // Get all completed Todo items
        public List<Todo> GetCompletedTodos()
        {
            return _Todos.Where(todo => todo.IsCompleted).ToList();
        }

        // Add a new Todo item to the list
        public void AddTodo(Todo todo)
        {
            if (todo != null)
            {
                _Todos.Add(todo);
            }
        }

        // Remove a Todo item from the list
        public void RemoveTodo(Todo todo)
        {
            if (todo != null)
            {
                _Todos.Remove(todo);
            }
        }

        //
    }
}
