using System;

namespace PrismProject.System2
{
    internal class Console
    {
        public static void WriteColoredLine(dynamic text, ConsoleColor color)
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
    }
}
