/* Console UI */

/* This static class provides methods for displaying information to the user and getting input. */


namespace TodoListProject
{
    public static class ConsoleUI
    {
        // Display welcome message with task counts
        public static void DisplayWelcomeMessage(int incompleteCount, int completedCount)
        {
            Console.WriteLine("Welcome to the Todo List Application!");
            Console.WriteLine($"You have {incompleteCount} tasks to do and {completedCount} completed tasks.");
        }

        // Display the main menu options
        public static void DisplayMainMenu()
        {
            Console.WriteLine("\nMain Menu - Pick an option:");
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
            Utilities.WriteColoredKey("(1) Show Todo List (by date or project)", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Add New Todo", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Edit Todo (update, mark as completed, remove)", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Save and Quit", "4", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
        }

        // Display the edit menu options
        public static void DisplayEditMenu()
        {
            Console.WriteLine("\nEdit Todo Menu - Pick an option:");
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkMagenta);
            Utilities.WriteColoredKey("(1) Update Todo Details", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Mark as Completed", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Remove Todo", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Return to Main Menu", "4", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkMagenta);
        }

        // Display the list menu options
        public static void DisplayListMenu()
        {
            Console.WriteLine("\nTodo List Menu - Pick an option:");
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkBlue);
            Utilities.WriteColoredKey("(1) Show Todos sorted by date", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Show Todos split by project", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Show only incomplete Todos", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Show only completed Todos", "4", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(5) Return to Main Menu", "5", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkBlue);
        }

        // Display a list of todos with optional IDs
        public static void DisplayListOfTodos(List<Todo> todos, string title, bool showIds = false)
        {
            if (todos.Count == 0)
            {
                Utilities.PrintStatementInColor("No Todos to display.", ConsoleColor.Yellow);
                return;
            }

            Utilities.PrintStatementInColor($"\n{title}", ConsoleColor.Cyan);

            if (showIds)
            {
                Utilities.PrintStatementInColor("=================================================================================\n", ConsoleColor.Cyan);

                Console.WriteLine($"{"ID",-10}{"Title",-30}{"Due Date",-15}{"Project",-15}{"Completed",-15}");
                Console.WriteLine($"{"---",-10}{"------",-30}{"---------",-15}{"--------",-15}{"----------",-15}");
            }
            else
            {
                Utilities.PrintStatementInColor("======================================================================\n", ConsoleColor.Cyan);
                Console.WriteLine($"{"Title",-30}{"Due Date",-15}{"Project",-15}{"Completed",-15}");
                Console.WriteLine($"{"------",-30}{"---------",-15}{"--------",-15}{"----------",-15}");
            }

            foreach (Todo todo in todos)
            {
                string status = todo.IsCompleted ? "Yes" : "No";
                if (showIds)
                {
                    Console.WriteLine($"{todo.Id,-10}{todo.Title,-30}{todo.DueDate.ToString("yyyy-MM-dd"),-15}{todo.ParentProjectName,-15}{status,-15}");
                }
                else
                {
                    Console.WriteLine($"{todo.Title,-30}{todo.DueDate.ToString("yyyy-MM-dd"),-15}{todo.ParentProjectName,-15}{status,-15}");
                }
            }
        }

        // Display a project along with its related todos
        public static void DisplayProjectWithTodos(Project project, TodoListManager manager)
        {
            Console.WriteLine($"\nProject: {project.Name}");
            Console.WriteLine("-----------------------");

            if (project.RelatedTodoIds.Count == 0)
            {
                Utilities.PrintStatementInColor("No Todos found for this project.", ConsoleColor.Yellow);
                return;
            }

            foreach (int todoId in project.RelatedTodoIds)
            {
                Todo todo = manager.GetTodoById(todoId);
                if (todo != null)
                {
                    Console.WriteLine($"- {todo.Title} (Due: {todo.DueDate.ToString("yyyy-MM-dd")}) / {(todo.IsCompleted ? "Done" : "Not done")}");
                }
            }
        }

        // Get user input as an integer
        public static int GetUserInputAsInt(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return -1; // Indicate invalid input
        }
    }
}
