using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.System.Graphics;

namespace LemonProject
{
    public class Cmds
    {
        public static int PixelHeight;
        public static int PixelWidth;
        public static List<string> cmds = new List<string>();

        public static void Parse(string input)
        {
            string[] args = input.Split(new char[0]);
            string[] cmdargs = { };
            if (input.Contains(" ")) { cmdargs = input.Remove(0, input.IndexOf(' ') + 1).Split(new char[0]); }
            if (!cmds.Contains(args[0])) { Utils.Error("Invalid command."); }

            if (args[0].Equals("print")) { print(cmdargs); }
            if (args[0].Equals("about")) { about(); }
            if (args[0].Equals("help")) { help(cmdargs); }
            if (args[0].Equals("shutdown")) { shutdown(cmdargs); }
            if (args[0].Equals("systime")) { systime(); }
            if (args[0].Equals("clear")) { clear(); }
            if (args[0].Equals("sysinfo")) { sysinfo(); }
            if (args[0].Equals("cursor")) { cursor(); }
        }

        public static void Init()
        {
            cmds.Add("print");
            cmds.Add("about");
            cmds.Add("help");
            cmds.Add("shutdown");
            cmds.Add("systime");
            cmds.Add("clear");
            cmds.Add("sysinfo");
            cmds.Add("cursor");
        }

        #region Misc Commands
        static void print(string[] args)
        {
            if (args.Length < 1)
            {
                Utils.Error("Insufficient arguments.");
            }
            string content = String.Join(" ", args);
            Console.WriteLine(content);
        }
        static void help(string[] args)
        {
            if (args.Length < 1)
            {
                Utils.colorCache = Console.ForegroundColor;
                Utils.SetColor(ConsoleColor.Cyan);
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine();
                Utils.SetColor(ConsoleColor.Blue);
                foreach (string cmd in cmds)
                {
                    Console.WriteLine(cmd);
                }
                Utils.SetColor(Utils.colorCache);
                Console.WriteLine();
                Console.WriteLine("You can get more specific help for each command by using: HELP <COMMAND_NAME>");
                Console.WriteLine();
            }
            else
            {
                // todo: add specific help for each command by checking args
            }
        }
        static void about()
        {
            Utils.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
___________________________________________________
  _                                   ____   _____ 
 | |                                 / __ \ / ____|
 | |     ___ _ __ ___   ___  _ __   | |  | | (___  
 | |    / _ \ '_ ` _ \ / _ \| '_ \  | |  | |\___ \ 
 | |___|  __/ | | | | | (_) | | | | | |__| |____) |
 |______\___|_| |_| |_|\___/|_| |_|  \____/|_____/
___________________________________________________
");
            Utils.SetColor(ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Lemon OS (c) 2021, release 1.2");
            Console.WriteLine("Created by bad-codr and deadlocust");
            Utils.Warn("This is a closed beta version of Lemon OS, we are not responsible for any damages caused by it.");
            Console.WriteLine();
            Utils.SetColor(ConsoleColor.White);
        }
        static void shutdown(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Shutdown machine? [Y/N]");
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Shutdown(); }
                Console.WriteLine();
                return;
            }
            else if (args[1] == "-r")
            {
                Console.WriteLine("Reboot machine? [Y/N]");
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Reboot(); }
                Console.WriteLine();
                return;
            }
            return;
        }
        static void systime()
        {
            Console.Write(Cosmos.HAL.RTC.Hour);
            Console.Write(":");
            Console.Write(Cosmos.HAL.RTC.Minute);
            Console.Write(":");
            Console.WriteLine(Cosmos.HAL.RTC.Second);
        }
        static void clear()
        {
            Console.Clear();
        }
        static void sysinfo()
        {
            var cspeed = Cosmos.Core.CPU.GetCPUCycleSpeed();
            var ram = Cosmos.Core.CPU.GetAmountOfRAM();
            Utils.syetem_message("CPU clock speed: " + cspeed + " Mhz");
            Utils.syetem_message("Total ram: " + ram + " MB");
        }
        static void cursor()
        {
            Utils.cursor();
        }
        #endregion
    }
}
