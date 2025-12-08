/* Utility methods */

namespace TodoListProject
{
    internal class Utilities
    {
        // NOTE TO SELF: Look at this later
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

            // 1. Write the part before the key
            Console.Write(text.Substring(0, index));

            // 2. Change color and write the key
            Console.ForegroundColor = color;
            Console.Write(key);

            // 3. Reset color and write the part after the key
            Console.ResetColor();
            Console.WriteLine(text.Substring(index + key.Length));
        }
    }
}
