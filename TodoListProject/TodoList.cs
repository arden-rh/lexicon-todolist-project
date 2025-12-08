

using TodoList;

namespace TodoListProject
{
    public class TodoList
    {
        private List<Todo> _Todos = new List<Todo>();

        //
        public void AddTodo(Todo todo)
        {
            if (todo != null)
            {
                _Todos.Add(todo);
            }
        }

        //
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
