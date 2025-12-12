/* Input Helper */

/// <summary>
/// Class <c>InputHelper</c> provides helper methods for input validation and error handling.
/// </summary>

namespace TodoListProject
{
    public static class InputHelper
    {
        // Error handling for empty input fields method
        public static bool IsInputFieldEmpty(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Input cannot be empty. Please try again.");
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

        // Get user input as an integer, mainly used for menu selections
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

        /// <summary>
        /// Validation methods
        /// These methods prompt the user for input, validate it, and check for quit commands.
        /// They prompt repeatedly until valid input is received or the user chooses to quit.
        /// </summary>

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
                if (IsInputFieldEmpty(Input))
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

        public static DateOnly GetValidatedDateInput(string Prompt, DateOnly previousDate, out bool IsQuit)
        {
            IsQuit = false;
            DateOnly InputDate;

            do
            {
                Console.WriteLine($"{Prompt}:");
                Utilities.PrintStatementInColor($"Current Value: {previousDate.ToString("yyyy-MM-dd")}", ConsoleColor.DarkGray);
                Console.Write("New Date (or press Enter to keep previous): ");
                string Input = Console.ReadLine().Trim();

                // Check for quit command
                if (IsQuitCommand(Input))
                {
                    IsQuit = true;
                    return default;
                }
                // Accept previous input if user presses Enter
                if (string.IsNullOrWhiteSpace(Input))
                {
                    return previousDate;
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
                if (IsInputFieldEmpty(Input))
                {
                    continue;
                }

                return Input;

            } while (true);
        }

        public static string GetValidatedStringInput(string Prompt, string previousInput, out bool IsQuit)
        {
            IsQuit = false;
            string Input = string.Empty;

            do
            {
                Console.WriteLine($"{Prompt}:");
                Utilities.PrintStatementInColor($"Current Value: {previousInput}", ConsoleColor.DarkGray);
                Console.Write("New Value (or press Enter to keep previous): ");
                Input = Console.ReadLine().Trim();

                // Check for quit command
                if (IsQuitCommand(Input))
                {
                    IsQuit = true;
                    return null;
                }

                // Accept previous input if user presses Enter
                if (string.IsNullOrWhiteSpace(Input))
                {
                    return previousInput;
                }

                return Input;

            } while (true);
        }

        // Get validated boolean input method
        public static bool GetValidatedBoolInput(string Prompt, bool previousInput, out bool IsQuit)
        {
            IsQuit = false;
            bool InputBool;
            do
            {
                Console.Write($"{Prompt} (true/false): ");
                string Input = Console.ReadLine().Trim();

                // Check for quit command
                if (IsQuitCommand(Input))
                {
                    IsQuit = true;
                    return default;
                }

                // Error handling for empty input
                if (IsInputFieldEmpty(Input))
                {
                    return previousInput;
                }

                // Try to parse the boolean
                if (!bool.TryParse(Input, out InputBool))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid input. Please enter 'true' or 'false'.");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                return InputBool;
            } while (true);
        }
    }
}



