using System;
using System.Collections.Generic;
using Cosmos.HAL;
using Cosmos.System.Graphics;

namespace PrismProject
{
    //remember, you can compress the lined down by pressing the [-] button on a colapsable line, to make it neater.
    //i just moves everything because this cs file is now the system core, everything lives here, instead of in chunks.
    //this also alows for a smaller size because we arent re-using the same code multiple times. the first few commits have theese comments so everybody knows whats going on, they will be removed later.
    //old size = 29 mb, new size = 28.1. it also feels a bit faster when using on a vm.

    class gui
    {
        public static Canvas canvas;
        public static int screenX = 800;
        public static int screenY = 800;

        public static void start()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Clear(System.Drawing.Color.AliceBlue);
        }

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(new Pen(System.Drawing.Color.Blue), 0, 0, screenX, screenY / 10);
        }

        public static void draw_menubtn()
        {

        }

        public static void mouse()
        {

        }
    } //GUI class for drawing objects. contains functions for specific shapes and system popups/pages.

    public class tools
    {
        public static ConsoleColor colorCache;
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
            tools.SetColor(ConsoleColor.Red);
            Console.WriteLine("Error: " + errorcontent);
            tools.SetColor(colorCache);
        }

        public static void Warn(string warncontent)
        {
            colorCache = Console.ForegroundColor;
            tools.SetColor(ConsoleColor.Yellow);
            Console.WriteLine("Warning: " + warncontent);
            tools.SetColor(colorCache);
        }

        public static void syetem_message(string message)
        {
            colorCache = Console.ForegroundColor;
            tools.SetColor(ConsoleColor.Magenta);
            Console.WriteLine(message);
            tools.SetColor(colorCache);
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
            {
                tools.Error("Insufficient arguments.");
            }
            if (args.Length > greaterthan)
            {
                tools.Error("too many arguments");
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
            tools.SetColor(ConsoleColor.Green);
            Console.WriteLine(args);
        }

        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }
    } //rename feom Utils, Still same functionality, just moved here so evrything is more tidy.

    public class Cmds
    {
        #region stuff
        public static int PixelHeight;
        public static int PixelWidth;
        public struct Command
        {
            public string Name, HelpDesc;
            public function func;
        }

        public static List<Command> cmds = new List<Command>();
        public delegate void function(string[] args);
        #endregion

        public static void Parse(string input)
        {
            string[] args = input.Split(new char[0]);
            string[] cmdargs = { };
            if (input.Contains(" ")) { cmdargs = input.Remove(0, input.IndexOf(' ') + 1).Split(new char[0]); }

            foreach (Command cmd in cmds)
            {
                if (args[0].Equals(cmd.Name))
                {
                    cmd.func(cmdargs);
                    return;
                }
            }

            tools.Error("Invalid command.");
        }

        private static void AddCommand(string name, string desc, function func)
        {
            Command cd = new Command();
            cd.Name = name;
            cd.HelpDesc = desc;
            cd.func = func;
            cmds.Add(cd);
        }

        public static void Init()
        {
            AddCommand("print", "Print any string of text, used for console applications.", print);
            AddCommand("about", "About prism OS", about);
            AddCommand("help", "List all available commands", help);
            AddCommand("shutdown", "Shuts down the system.\nArguments\n==========\n-r restarts the system instead", shutdown);
            AddCommand("clear", "clear entire console", clear);
            AddCommand("sysinfo", "Prints system information", sysinfo);
            AddCommand("tone", "used to make sounds in an app", tone);
        }

        #region Misc Commands
        static void print(string[] args)
        {
            if (args.Length < 1)
            {
                tools.Error("Insufficient arguments.");
            }
            string content = String.Join(" ", args);
            Console.WriteLine(content);
        }
        static void tone(string[] args)
        {
            tools.argcheck(args, 2, 2);
            int beep1 = int.Parse(args[0]);
            int beep2 = int.Parse(args[1]);
            Console.Beep(beep1, beep2);
        }
        static void help(string[] args)
        {
            if (args.Length < 1)
            {
                tools.colorCache = Console.ForegroundColor;
                tools.SetColor(ConsoleColor.Cyan);
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine("=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.WriteLine();
                tools.SetColor(ConsoleColor.Blue);
                foreach (Command cmd in cmds)
                {
                    Console.WriteLine(cmd.Name);
                }
                tools.SetColor(tools.colorCache);
                Console.WriteLine();
                Console.WriteLine("You can get more specific help for each command by using: HELP <COMMAND_NAME>");
                Console.WriteLine();
            }
            else
            {
                foreach (Command cmd in cmds)
                {
                    if (args[0] == cmd.Name)
                    {
                        Console.WriteLine(cmd.HelpDesc);
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }
        static void about(string[] args)
        {
            tools.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(@"
_______________________________________________
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/                                                
_______________________________________________
");
            tools.SetColor(ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Prism OS (c) 2021, release " + Kernel.versionID);
            Console.WriteLine("Created by bad-codr and deadlocust");
            tools.Warn("This is a closed beta version of Prism OS, we are not responsible for any damages caused by it.");
            Console.WriteLine();
            tools.SetColor(ConsoleColor.White);
        }
        static void shutdown(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(@"Shutdown machine?
 ____       ____ 
||y ||   / ||n ||
||__||  /  ||__||
|/__\| /   |/__\|"
                );
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Shutdown(); }
                Console.WriteLine();
                return;
            }
            else if (args[0] == "-r")
            {
                Console.WriteLine(@"Reboot machine?
 ____       ____ 
||y ||   / ||n ||
||__||  /  ||__||
|/__\| /   |/__\|");
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'Y' || input.KeyChar == 'y') { Cosmos.System.Power.Reboot(); }
                Console.WriteLine();
                return;
            }
            else if (args[0] == "-t")
            {
                tools.Warn("shutting down in " + args[1] + " seconds");
                int time = Convert.ToInt32(args[1]);
                tools.Sleep(time);
                Cosmos.System.Power.Shutdown();
            }
            return;
        }
        static void clear(string[] args)
        {
            Console.Clear();
        }
        static void sysinfo(string[] args)
        {
            var cspeed = Cosmos.Core.CPU.GetCPUCycleSpeed();
            var ram = Cosmos.Core.CPU.GetAmountOfRAM();
            tools.syetem_message("CPU clock speed: " + cspeed + " Mhz");
            tools.syetem_message("Total ram: " + ram + " MB");
        }
        #endregion
    } //Still same functionality, just moved here so evrything is more tidy.

    public class filesystem
    {

    } //filesystem class, used for acessing files. comming soon.

    public class networking
    {

    } //networking class, used for acessing the internet. comming soon.
}
