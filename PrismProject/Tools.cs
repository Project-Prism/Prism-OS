using System;
using Cosmos.HAL;
using Console = System.Console;

namespace PrismProject
{
    public class Tools
    {
        public static ConsoleColor colorCache;
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

        public static void Error(string errorcontent)
        {
            colorCache = Console.ForegroundColor;
            SetColor(ConsoleColor.Red);

            Console.WriteLine("Error: " + errorcontent);
            SetColor(colorCache);
        }

        public static void Warn(string warncontent)
        {
            colorCache = Console.ForegroundColor;
            SetColor(ConsoleColor.Yellow);
            Console.WriteLine("Warning: " + warncontent);
            SetColor(colorCache);
        }

        public static void syetem_message(string message)
        {
            colorCache = Console.ForegroundColor;
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(message);
            SetColor(colorCache);
        }

        //only works when in terminal mode, must find another way to play an audio file.
        public static void Playjingle()
        {
            Console.Beep(2000, 100);
            Console.Beep(2500, 100);
            Console.Beep(3000, 100);
            Console.Beep(2500, 100);
        }

        public static void argcheck(string[] args, int lessthan, int greaterthan)
        {
            if (args.Length < lessthan)
                Error("Insufficient arguments.");
            else if (args.Length > greaterthan)
                Error("too many arguments");
        }

        public static void debug(string[] args)
        {
            Console.Write("CPU vendor: ");
            Console.WriteLine(Cosmos.Core.CPU.GetCPUVendorName());

            Console.Write("Uptime: ");
            Console.WriteLine(Cosmos.Core.CPU.GetCPUUptime());

            Console.Write("Kernel Interupts: ");
            Console.WriteLine(Cosmos.System.Kernel.InterruptsEnabled);

            Console.Write("Intel vendor ID: ");
            Console.WriteLine(VendorID.Intel);
        }

        public static void success(string args)
        {
            SetColor(ConsoleColor.Green);
            Console.WriteLine(args);
        }

        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }
    }
}
