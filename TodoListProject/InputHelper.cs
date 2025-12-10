
/* Input Helper */

/* This static class provides helper methods for input validation and error handling. */

namespace TodoListProject
{


    public static class InputHelper
    {
        // Error handling for empty input fields method
        public static bool IsInputFieldEmpty(string Input, string FieldName)
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{FieldName} cannot be empty. Please try again.");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            return false;
        }

        // Check for quit command method
        public static bool IsQuitCommand(string Input)
        {
            return Input.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase);
        }

        // Get validated DateOnly input method
        public static DateOnly GetValidatedDateInput(string Prompt, out bool IsQuit)
        {
            IsQuit = false;
            DateOnly InputDate;

            do
            {
                Console.Write($"{Prompt} (YYYY-MM-DD): ");
                string Input = Console.ReadLine().Trim();

                // Check for quit command
                if (IsQuitCommand(Input))
                {
                    IsQuit = true;
                    return default;
                }
                // Error handling for empty input
                if (IsInputFieldEmpty(Input, Prompt))
                {
                    continue;
                }
                // Try to parse the date
                if (!DateOnly.TryParse(Input, out InputDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid date. Please enter a valid date.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                return InputDate;

            } while (true);
        }

        // Get validated string input method
        // This method prompts the user for input, validates it, and checks for quit command,
        // returning the input or indicating if the user chose to quit.
        public static string GetValidatedStringInput(string Prompt, out bool IsQuit)
        {
            IsQuit = false;
            string Input = string.Empty;

            do
            {
                Console.Write($"{Prompt}: ");
                Input = Console.ReadLine().Trim();

                // Check for quit command
                if (IsQuitCommand(Input))
                {
                    IsQuit = true;
                    return null;
                }

                // Error handling for empty input
                if (IsInputFieldEmpty(Input, Prompt))
                {
                    continue;
                }

                return Input;

            } while (true);
        }
    }
}



