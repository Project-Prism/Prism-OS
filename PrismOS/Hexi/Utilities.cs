using System.Collections.Generic;
using System.Text;

namespace PrismOS.Hexi
{
    internal static class Utilities
    {
        internal enum Code : byte
        {
            // Interface
            Write, // Write text. [code][value]
            WriteLine, // Write text with newline [code][value]

            // Memory
            Allocate, // Allocate a specific ammount of memory to the program [code][value]
            MemSet, // Set a part of memory to a value [code][value][value]

            // Core
            Jump, // Jump to a specific byte of the code [code][value]
            Quit, // Stop the program [code]
        }

        internal static void AddString(ref List<byte> Bytes, string String)
        {
            Bytes.Add((byte)String.Length);
            foreach (char Char in String)
            {
                Bytes.Add((byte)Char);
            }
        }

        internal static string[] SplitCsv(string String)
        {
            List<string> result = new();
            StringBuilder currentStr = new("");
            bool inQuotes = false;
            for (int i = 0; i < String.Length; i++) // For each character
            {
                if (String[i] == '\"') // Quotes are closing or opening
                {
                    inQuotes = !inQuotes;
                }
                else if (String[i] == ',') // Comma
                {
                    if (!inQuotes) // If not in quotes, end of current string, add it to result
                    {
                        result.Add(currentStr.ToString());
                        currentStr.Clear();
                    }
                    else
                    {
                        currentStr.Append(String[i]); // If in quotes, just add it 
                    }
                }
                else // Add any other character to current string
                {
                    currentStr.Append(String[i]);
                }
            }
            result.Add(currentStr.ToString());
            return result.ToArray(); // Return array of all strings
        }
    }
}