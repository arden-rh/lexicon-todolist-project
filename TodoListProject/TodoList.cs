/* Todo List */

namespace TodoListProject
{
    public class TodoList
    {
        public List<Todo> Todos { get; private set; }
        public List<Project> Projects { get; private set; }

        public TodoList(List<Todo> todos, List<Project> projects)
        {
            Todos = todos;
            Projects = projects;
        }

        // Get all Todo items
        public void GetListOfAllTodos()
        {
            // Sort assets before presenting the list
            // SortedAssets = SortAssets(Assets);

            Console.WriteLine("\nList of Todos:");
            Console.WriteLine("----------------");
            Console.WriteLine($"{"Title",-30}{"Due Date",-15}{"Project",-15}{"Completed",-15}");
            Console.WriteLine($"{"-----",-30}{"---------",-15}{"--------",-15}{"----------",-15}");

            // Loop through list, checking EndOfLife date and applying color as appropiate
            foreach (Todo todo in Todos)
            {
                Console.WriteLine($"{todo.Title,-30}{todo.DueDate.ToString("yyyy-MM-dd"),-15}{todo.ParentProjectName,-15}{todo.IsCompleted,-15}");
            }
            Console.WriteLine();
            //Console.WriteLine(

        }

        // Get all incomplete Todo items
        public List<Todo> GetIncompleteTodos()
        {
            return Todos.Where(todo => !todo.IsCompleted).ToList();
        }

        // Get all completed Todo items
        public List<Todo> GetCompletedTodos()
        {
            return Todos.Where(todo => todo.IsCompleted).ToList();
        }

        // Add a new Todo item to the list
        public void AddTodo()
        {

            bool isQuit;
            Console.WriteLine("Add a new Todo");
            Console.WriteLine("-------------");

            string ProjectName;

            if (Projects != null)
            {
                Console.WriteLine($"List of current projects: ");
                foreach (Project project in Projects)
                {
                    Console.WriteLine($"- {project.Name}");
                }
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
            DateOnly DueDate = InputHelper.GetValidatedDateInput("Enter the due date of the new Todo", out isQuit);
            if (isQuit)
            {
                return;
            }


            Project NewProject;
            Todo NewTodo;

            if (Projects != null)
            {
                Project ChosenProject = Projects.Find(p => p.Name == ProjectName);
                if (ChosenProject != null)
                {
                    NewTodo = new Todo(Title, DueDate, ChosenProject.Name);
                    ChosenProject.RelatedTodoIds.Add(NewTodo.Id);
                }
                else
                {
                    NewProject = new Project(ProjectName);
                    Projects.Add(NewProject);
                    NewTodo = new Todo(Title, DueDate, NewProject.Name);
                    NewProject.RelatedTodoIds.Add(NewTodo.Id);
                }
            }
            else
            {
                NewProject = new Project(ProjectName);
                NewTodo = new Todo(Title, DueDate, NewProject.Name);
                NewProject.RelatedTodoIds.Add(NewTodo.Id);
            }
            
            Todos.Add(NewTodo);
        }


        // Remove a Todo item from the list
        public void RemoveTodo(string todoTitle)
        {
            Todo? Todo = Todos.Find(t => t.Title == todoTitle);

            if (Todo == null)
            {
                Utilities.PrintStatementInColor("Todo not found.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("Is this the todo you want to remove?");
            Console.WriteLine($"Title: {Todo.Title}, Due Date: {Todo.DueDate}, Project: {Todo.ParentProjectName}, Completed: {Todo.IsCompleted}");
            Console.Write("(Y)es / (N)o: ");
            string Confirmation = Console.ReadLine().Trim().ToUpper();

            if (Confirmation == "N" && Confirmation != "NO")
            {
                Utilities.PrintStatementInColor("Todo removal cancelled", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            if (Confirmation == "Y" || Confirmation == "YES")
            {
                Utilities.PrintStatementInColor("Todo removed successfully.", ConsoleColor.Green);
                Todos.Remove(Todo);
            }
        }
    }
}
