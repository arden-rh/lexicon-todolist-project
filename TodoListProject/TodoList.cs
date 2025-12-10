/* Todo List */

namespace TodoListProject
{
    public class TodoList
    {
        public List<Todo> Todos { get; private set; }
        public List<Project> Projects { get; private set; }

        public TodoList(List<Todo> todos, List<Project> projects)
        {
            Todos = todos ?? new List<Todo>();
            Projects = projects ?? new List<Project>();
        }

        // Get all Todo items
        public void GetListOfAllTodos(bool sortByDate = false)
        {
            List<Todo> sortedTodos;

            // Sort todos by project name and due date
            if (sortByDate)
            {
                sortedTodos = Todos.OrderBy(todo => todo.DueDate).ToList();
            }
            else
            {
                sortedTodos = Todos.OrderBy(todo => todo.ParentProjectName).ThenBy(todo => todo.DueDate).ToList();
            }

            ConsoleUI.DisplayListOfTodos(sortedTodos, "List of Todos:");
        
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

        /* Add a new Todo item to the list */
        public bool AddTodo()
        {

            bool isQuit;
            Console.WriteLine("\nAdd a new Todo");
            Utilities.PrintStatementInColor("Type 'Q' at any time to return to the main menu.", ConsoleColor.Yellow);
            Console.WriteLine("---------------------------------------------------");

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
                    return false;
                }
            }
            else
            {
                ProjectName = InputHelper.GetValidatedStringInput("Enter the project name of the new Todo", out isQuit);
                if (isQuit)
                {
                    return false;
                }
            }

            string Title = InputHelper.GetValidatedStringInput("Enter the title of the new Todo", out isQuit);
            if (isQuit)
            {
                return false;
            }
            DateOnly DueDate = InputHelper.GetValidatedDateInput("Enter the due date of the new Todo", out isQuit);
            if (isQuit)
            {
                return false;
            }

            Project ChosenProject = Projects.FirstOrDefault(p => p.Name.Equals(ProjectName, StringComparison.OrdinalIgnoreCase));

            Project TargetProject;

            if (ChosenProject != null)
            {
                TargetProject = ChosenProject;
            }
            else
            {
                TargetProject = new Project(ProjectName);
                Projects.Add(TargetProject);
                Utilities.PrintStatementInColor($"New project '{ProjectName}' created.", ConsoleColor.DarkGreen);
            }

            // Create the new Todo
            Todo NewTodo = new Todo(Title, DueDate, TargetProject.Name);

            // Link the new Todo to the Project
            TargetProject.RelatedTodoIds.Add(NewTodo.Id);

            // Add the new Todo to the Todo list
            Todos.Add(NewTodo);

            Utilities.PrintStatementInColor($"Todo '{Title}' added successfully.", ConsoleColor.Green);
            return true;
        }


        /* Remove a Todo item from the list */
        public void RemoveTodo(string todoTitle)
        {
            // Find all todos matching the given title (case-insensitive)
            List<Todo> MatchingTodos = Todos.Where(t => t.Title.Equals(todoTitle, StringComparison.OrdinalIgnoreCase)).ToList();

            Todo TodoToRemove = null;
            int MatchingCount = MatchingTodos.Count();

            // Handle different cases based on the number of matching todos
            // Found exactly one matching todo - proceed to confirmation
            if (MatchingCount == 1)
            {
                TodoToRemove = MatchingTodos.First();
            }
            else if (MatchingCount == 0)
            {
                Utilities.PrintStatementInColor("Todo not found.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }
            else
            {
                // Rare case: multiple todos with the same title
                Console.WriteLine("Multiple todos found with the same title. Please select the one you want to remove:");
                foreach (Todo t in MatchingTodos)
                {
                    Console.WriteLine($"{t.Id} {t.Title} (Due: {t.DueDate}, Project: {t.ParentProjectName}, Completed: {t.IsCompleted})");

                }

                string UserInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(UserInput, out int SelectedId))
                {
                    TodoToRemove = MatchingTodos.FirstOrDefault(t => t.Id == SelectedId);
                    return;
                }

                if (TodoToRemove == null)
                {
                    Utilities.PrintStatementInColor("Todo not found. Todo removal cancelled.", ConsoleColor.Red);
                    Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                    return;
                }
            }

            // Confirm removal with the user
            Console.WriteLine("Is this the todo you want to remove?");
            Console.WriteLine($"Title: {TodoToRemove.Title}, Due Date: {TodoToRemove.DueDate}, Project: {TodoToRemove.ParentProjectName}, Completed: {TodoToRemove.IsCompleted}");
            string Confirmation = InputHelper.GetValidatedStringInput("(Y)es / (N)o: ", out bool isQuit);

            // User chose to not remove the todo or quit
            if (isQuit || Confirmation.Equals("N", StringComparison.OrdinalIgnoreCase) || Confirmation.Equals("NO", StringComparison.OrdinalIgnoreCase))
            {
                Utilities.PrintStatementInColor("Todo removal cancelled", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            // User confirmed removal
            if (Confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase) || Confirmation.Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                if (Projects != null)
                {
                    Project? ParentProject = Projects.FirstOrDefault(p => p.Name == TodoToRemove.ParentProjectName);

                    // Remove the todo ID from the parent project's related todo IDs
                    if (ParentProject != null)
                    {
                        ParentProject.RelatedTodoIds.Remove(TodoToRemove.Id);

                        // If the parent project has no more related todos, remove the project as well
                        if (ParentProject.RelatedTodoIds.Count == 0)
                        {
                            Projects.Remove(ParentProject);
                            Utilities.PrintStatementInColor($"Project '{ParentProject.Name}' has no more related todos and has been removed.", ConsoleColor.Yellow);
                        }
                    }
                }

                // Remove the todo from the list
                Todos.Remove(TodoToRemove);
                Utilities.PrintStatementInColor("Todo removed successfully.", ConsoleColor.Green);
            }
        }

        /* Mark a Todo item as completed */
        public bool MarkTodoAsCompleted(string todoTitle)
        {
            Todo? Todo = Todos.FirstOrDefault(t => t.Title == todoTitle);
            if (Todo == null)
            {
                Utilities.PrintStatementInColor("Todo not found.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return false;
            }
            if (Todo.IsCompleted)
            {
                Utilities.PrintStatementInColor("Todo is already marked as completed.", ConsoleColor.Yellow);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return false;
            }
            Todo.MarkAsCompleted();
            Utilities.PrintStatementInColor("Todo marked as completed successfully.", ConsoleColor.Green);
            return true;
        }
    }
}
