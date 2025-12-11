/* Utilities */

/// <summary>
/// Class <c>Utilities</c> provides various utility functions for the application.
/// </summary>
/// 
namespace TodoListProject
{
    internal class Utilities
    {
        // Print statement in color
        public static void PrintStatementInColor(string Statement, ConsoleColor Color, bool NewLine = true)
        {
            Console.ForegroundColor = Color;
            if (NewLine)
            {
                Console.WriteLine(Statement);
            }
            else
            {
                Console.Write(Statement);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Write text with a specific key highlighted in color
        public static void WriteColoredKey(string text, string key, ConsoleColor color)
        {
            // Find where the key is in the text
            int index = text.IndexOf(key);

            if (index == -1)
            {
                // If the key isn't found, just write the text normally
                Console.WriteLine(text);
                return;
            }

            // Write the part before the key
            Console.Write(text.Substring(0, index));

            // Change color and write the key
            Console.ForegroundColor = color;
            Console.Write(key);

            // Reset color and write the part after the key
            Console.ResetColor();
            Console.WriteLine(text.Substring(index + key.Length));
        }

        // Generate a random ID
        public static int GenerateNextFreeId(IEnumerable<int> existingIds)
        {
            // Use a SortedSet for efficient lookup
            var usedIds = new SortedSet<int>(existingIds.Where(id => id > 0));
            
            int newId = 1;

            // Find the smallest unused ID
            while (usedIds.Contains(newId))
            {
                newId++;
            }

            return newId;
        }
    }
}
