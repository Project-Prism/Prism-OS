using Cosmos.HAL;
using System;
    
namespace PrismProject
{
    public class Utils
    {
        public static ConsoleColor colorCache;
        
        //message based commands will be depricaeted soon, will be replaced with gui based messages.
        public static void Sleep(int secNum)
        {
            int StartSec = RTC.Second;
            int EndSec;
            if (StartSec + secNum > 59)
            {
                EndSec = 0;
            }
            else
            {
                EndSec = StartSec + secNum;
            }
            while (RTC.Second != EndSec)
            {
                // Loop round
            }
        }

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
            {
                Utils.Error("Insufficient arguments.");
            }
            if (args.Length > greaterthan)
            {
                Utils.Error("too many arguments");
            }
        }

        public static void debug(string[] args)
        {
            Console.Write("CPU vendor: ");
            Console.WriteLine(Cosmos.Core.ProcessorInformation.GetVendorName());
            Console.Write("Uptime: ");
            Console.WriteLine(Cosmos.Core.CPU.GetCPUUptime());
            Console.Write("Kernel Interupts: ");
            Console.WriteLine(Cosmos.System.Kernel.InterruptsEnabled);
            Console.Write("Intel vendor ID: ");
            Console.WriteLine(Cosmos.HAL.VendorID.Intel);
        }

        public static void success(string args)
        {
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine(args);
        }

        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }
    }
}
