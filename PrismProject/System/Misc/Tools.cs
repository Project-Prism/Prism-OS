using Cosmos.HAL;
using System;
using Console = System.Console;

namespace PrismProject
{
    public class Tools
    {
        public static ConsoleColor colorCache;
        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }

        public static void Sleep(int secNum)
        {
            int StartSec = RTC.Second;
            int EndSec;

            if (StartSec + secNum > 59)
                EndSec = 0;
            else
                EndSec = StartSec + secNum;

            // Loop round
            while (RTC.Second != EndSec) ;
        }

        public static void Length(string[] args, int lessthan, int greaterthan)
        {
            if (args.Length < lessthan)
                Console.WriteLine("Insufficient arguments.");
            else if (args.Length > greaterthan)
                Console.WriteLine("too many arguments");
        }
    }
}
