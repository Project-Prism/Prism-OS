using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Text;
using Console = System.Console;

namespace PrismProject
{
    public class Cmds
    {
        #region Variables
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
            Console.WriteLine("Invalid command.");
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
            cmds.Clear();

            //basic commands
            AddCommand("print", "Print any string of text, used for console applications.", print);
            AddCommand("about", "About prism OS", about);
            AddCommand("help", "List all available commands", help);
            AddCommand("shutdown", "Shuts down the system.\n    Arguments:\n       [ -r ] Restart\n       [ -t ] timed shutdown\n       [ -t TIME -r ] timed restart", shutdown);
            AddCommand("clear", "clear entire console", clear);
            AddCommand("sysinfo", "Prints system information", sysinfo);
            AddCommand("tone", "used to make sounds in an app", tone);
            AddCommand("aboutmem", "Information about system memory", meminfo);
            AddCommand("OOMtest", "A test for the OutOfMemory class", OOMtest);
            //filesystem
            AddCommand("makedir", "create a folder on the selected disk", Create_Directory);
            AddCommand("listdir", "List all files in a directory", List_Directory);
            AddCommand("read", "Read all text data from a file on the disk", Read_file);
            AddCommand("write", "Write text to a file.\n    Arguments:\n        PATH: full path to file\n       CONTENTS: data to be written", Write_file);
            AddCommand("format", "Format a drive to fat32\n    Arguments:\n        Drive id: specify the drive id\n        quick format: true or false", format);
            //graphics
            AddCommand("gui", "Loads the GUI.", Gui);
            //keyboard
            AddCommand("keymap", "Change keyboard settings.\nArguments\n==========\nlayout\n==========\nfr for french layout, us for english layout and de for german layout", Keyboard);
            //networking commands
            AddCommand("dhcp", "Sends a DHCP discover packet.", dhcp);
            AddCommand("ping", "Sends an ICMP packet and returns the elapsed time.\nArguments\n==========\n<address>", ping);
            AddCommand("tcp", "Sends a TCP packet and returns the response body as a UTF-8 string.\nArguments\n==========\n<address> <port> <timeout> <body>", tcp);
        }
        private static void Keyboard(string[] args)
        {
            throw new NotImplementedException();
        }
        public static void Input()
        {
            List<string> cmdlist = new List<string>();
            while (true)
            {
                Console.Write("> ");
                var cmd = Console.ReadLine();
                cmdlist.Add(cmd);
                Parse(cmd);
            }
        }

        #region Misc Commands
        static void Gui(string[] args)
        {
            Desktop.Start();
        }
        static void Keyboard(string type, string[] args)
        {
            if (type == "layout")
            {
                var layout = args[0];
                if (layout == "FR")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FR_Standard());
                else if (layout == "US")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.US_Standard());
                else if (layout == "DE")
                    KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DE_Standard());
                Console.WriteLine("Successfully set the keyboard layout to " + layout + "!");
            }
        }
        static void print(string[] args)
        {
            if (args.Length < 1)
                Console.WriteLine("Insufficient arguments.");

            string content = string.Join(" ", args);
            Console.WriteLine(content);
        }
        static void tone(string[] args)
        {
            Tools.Length(args, 2, 2);
            int beep1 = int.Parse(args[0]);
            int beep2 = int.Parse(args[1]);
            Console.Beep(beep1, beep2);
        }
        static void help(string[] args)
        {
            if (args.Length < 1)
            {
                Tools.colorCache = Console.ForegroundColor;
                Tools.SetColor(ConsoleColor.Cyan);
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine("=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.WriteLine();
                Tools.SetColor(ConsoleColor.Blue);

                foreach (Command cmd in cmds)
                    Console.WriteLine(cmd.Name);

                Tools.SetColor(Tools.colorCache);
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
            Tools.SetColor(ConsoleColor.Yellow);
            Console.WriteLine(Images.art1);
            Tools.SetColor(ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Prism OS (c) 2021, release " + Kernel.Kernel_build);
            Console.WriteLine("Created by bad-codr and deadlocust");
            Console.WriteLine("This is a closed beta version of Prism OS, we are not responsible for any damages caused by it.");
            Console.WriteLine();
            Tools.SetColor(ConsoleColor.White);
        }
        static void shutdown(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "-r")
                {
                    Power.Reboot();
                }
                else if (args[0] == "-t" && args[2] == "-r")
                {
                    Tools.Sleep(Convert.ToInt32(args[1]));
                    Power.Reboot();
                }
                else if (args[0] == "-t")
                {
                    Tools.Sleep(Convert.ToInt32(args[1]));
                    Power.Shutdown();
                }

            }
        }
        static void clear(string[] args)
        {
            Console.Clear();
        }
        static void sysinfo(string[] args)
        {
            var cspeed = Cosmos.Core.CPU.GetCPUCycleSpeed();
            var ram = Cosmos.Core.CPU.GetAmountOfRAM();
            Console.WriteLine("CPU clock speed: " + (cspeed / 1000 / 1000) + " Mhz");
            Console.WriteLine("Total ram: " + ram + " MB");
        }
        //memory commands
        static void meminfo(string[] args)
        {
            Console.WriteLine("Used memory: " + Memory.Used + "MB");
            Console.WriteLine("Free memory: " + Memory.Free + "MB");
            Console.WriteLine("Total memory: " + Memory.Total + "MB");
        }
        static void OOMtest(string[] args)
        {
            Memory.OutOfMemoryWarning();
        }

        //filesystem commands
        static void List_Directory(string[] path)
        {
            Console.WriteLine(Filesystem.List_Directory(path[0]));
        }
        static void Create_Directory(string[] path)
        {
            Filesystem.Create_driectory(path[0]);
        }
        static void Read_file(string[] path)
        {
            Console.WriteLine(Filesystem.Read_file(path[0]));
        }
        static void Write_file(string[] args)
        {
            Filesystem.Write(args[0], args[1]);
        }
        static void format(string[] args)
        {
            Filesystem.format(args[0], Convert.ToBoolean(args[1]));
        }

        //Network related commands
        static void dhcp(string[] args)
        {
            Networking.DHCP();
        }
        static void ping(string[] args)
        {
            Console.WriteLine(Networking.Ping(args[0]).ToString());
        }
        static void tcp(string[] args)
        {
            Console.WriteLine(Encoding.UTF8.GetString(Networking.tcp(args[0], int.Parse(args[1]), int.Parse(args[2]), args[3])));
        }
        #endregion
    }
}
