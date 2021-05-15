using System;
    
namespace PrismProject
{
    public class Utils
    {
        public static ConsoleColor colorCache;

        public static void Error(string errorcontent)
        {
            colorCache = Console.ForegroundColor;
            Utils.SetColor(ConsoleColor.Red);
            Console.WriteLine("Error: " + errorcontent);
            Utils.SetColor(colorCache);
        }

        public static void Warn(string warncontent)
        {
            colorCache = Console.ForegroundColor;
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine("Warning: " + warncontent);
            Utils.SetColor(colorCache);
        }

        public static void syetem_message(string message)
        {
            colorCache = Console.ForegroundColor;
            Utils.SetColor(ConsoleColor.Magenta);
            Console.WriteLine(message);
            Utils.SetColor(colorCache);
        }

    public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }
    }
}
