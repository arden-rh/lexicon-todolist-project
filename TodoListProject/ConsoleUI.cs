/* Console UI */

namespace TodoListProject
{
    public class ConsoleUI
    {
        public static void DisplayWelcomeMessage(int incompleteCount, int completedCount)
        {
            Console.WriteLine("Welcome to the Todo List Application!");
            Console.WriteLine($"You have {incompleteCount} tasks to do and {completedCount} completed tasks.");
            Console.WriteLine("-----------------------------------------------");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("Pick an option:");
            Utilities.WriteColoredKey("(1) Show Todo List (by date or project)", "1", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(2) Add New Todo", "2", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(3) Edit Todo (update, mark as completed, remove)", "3", ConsoleColor.DarkCyan);
            Utilities.WriteColoredKey("(4) Save and Quit", "4", ConsoleColor.DarkCyan);
        }

        public static int GetUserChoice()
        {
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            return -1; // Invalid choice
        }


    }
}
