using Cosmos.HAL;
using System;
using System.Net;
using Console = System.Console;

namespace PrismProject
{
    public class Tools
    {
        public static ConsoleColor colorCache;
        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }

        public static void Sleep(int Until)
        {
            int Starton = RTC.Second;
            int EndSec;

            if (Starton + Until > 59)
                EndSec = 0;
            else
                EndSec = Starton + Until;

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
        public static bool IsIPAddress(string ipAddress)
        {
            try
            {
                IPAddress address;
                return IPAddress.TryParse(ipAddress, out address);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
