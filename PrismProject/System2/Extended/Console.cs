using System;

namespace PrismProject.System2.Extended
{
    /// <summary>
    /// system2 console patches.
    /// </summary>
    internal class Console
    {
        public static void WriteColoredLine(string text, ConsoleColor color)
        {
            ConsoleColor color1 = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = color1;
        }

        public static void WriteColored(dynamic text, ConsoleColor color)
        {
            ConsoleColor color1 = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.Write(text);
            System.Console.ForegroundColor = color1;
        }

        public static void CheckTextLength(string text, int MinLength, int MaxLength)
        {
            if (text.Length < MinLength)
                System.Console.WriteLine("Insufficient arguments.");
            else if (text.Length > MaxLength)
                System.Console.WriteLine("too many arguments");
        }
    }
}