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
        public void AddTodo(List<Project> ListOfProjects = null)
        {

            bool isQuit;
            Console.WriteLine("Add a new Todo");
            Console.WriteLine("-------------");

            string ProjectName;

            if (ListOfProjects != null)
            {
                Console.WriteLine($"List of current projects: {string.Join(", ", ListOfProjects)}");
                ProjectName = InputHelper.GetValidatedStringInput("Enter a new project name for the new Todo or one of the ones listed above", out isQuit);
                if (isQuit)
                {
                    return;
                }
            }
            else
            {
                ProjectName = InputHelper.GetValidatedStringInput("Enter the project name of the new Todo", out isQuit);
                if (isQuit)
                {
                    return;
                }
            }

            string Title = InputHelper.GetValidatedStringInput("Enter the title of the new Todo", out isQuit);
            if (isQuit)
            {
                return;
            }
            DateTime DueDate = InputHelper.GetValidatedDateTimeInput("Enter the due date of the new Todo", out isQuit);
            if (isQuit)
            {
                return;
            }


            Project NewProject;
            Todo NewTodo;

            if (ListOfProjects != null)
            {
                Project ChosenProject = ListOfProjects.Find(p => p.Name == ProjectName);
                if (ChosenProject != null)
                {
                    NewTodo = new Todo(Title, DueDate, ChosenProject);
                    ChosenProject.Todos.Add(NewTodo);
                }
                else
                {
                    NewProject = new Project(ProjectName);
                    NewTodo = new Todo(Title, DueDate, NewProject);
                    NewProject.Todos.Add(NewTodo);
                }
            }
            else
            {
                NewProject = new Project(ProjectName);
                NewTodo = new Todo(Title, DueDate, NewProject);
                NewProject.Todos.Add(NewTodo);
            }
            
            _Todos.Add(NewTodo);
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
