/* Console UI */

namespace TodoListProject
{
    public class ConsoleUI
    {
        public static void DisplayWelcomeMessage(int incompleteCount, int completedCount)
        {
            Console.WriteLine("Welcome to the Todo List Application!");
            Console.WriteLine($"You have {incompleteCount} tasks to do and {completedCount} completed tasks.");
        }

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

        public static void DisplayEditMenu()
        {
            Console.WriteLine("\nEdit Todo Menu - Pick an option:");
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
            Utilities.WriteColoredKey("(1) Update Todo Details", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Mark as Completed", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Remove Todo", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Return to Main Menu", "4", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
        }

        public static void DisplayListMenu()
        {
            Console.WriteLine("\nTodo List Menu - Pick an option:");
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
            Utilities.WriteColoredKey("(1) Show Todos sorted by date only", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Show Todos sorted by project and due date", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Show only incomplete Todos", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Show only completed Todos", "4", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(5) Return to Main Menu", "5", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("-------------------------------------------------", ConsoleColor.DarkYellow);
        }

        public static void DisplayListOfTodos(List<Todo> todos, string title)
        {
            if (todos.Count == 0)
            {
                Utilities.PrintStatementInColor("No Todos to display.", ConsoleColor.Yellow);
                return;
            }

            Utilities.PrintStatementInColor($"\n{title}", ConsoleColor.Cyan);
            Utilities.PrintStatementInColor("======================================================================\n", ConsoleColor.Cyan);
            Console.WriteLine($"{"Title",-30}{"Due Date",-15}{"Project",-15}{"Completed",-15}");
            Console.WriteLine($"{"-----",-30}{"---------",-15}{"--------",-15}{"----------",-15}");

            foreach (Todo todo in todos)
            {
                string status = todo.IsCompleted ? "Completed" : "Incomplete";
                Console.WriteLine($"{todo.Title,-30}{todo.DueDate.ToString("yyyy-MM-dd"),-15}{todo.ParentProjectName,-15}{status,-15}");
            }
        }

        public static int GetUserMenuChoice()
        {
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            return -1; // Invalid choice
        }

        public static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }


    }
}
