using System;
using System.Collections.Generic;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using Console = System.Console;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.Network;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using System.Text;

namespace PrismProject
{
    //remember, you can compress the line down by pressing the [-] button on a colapsable line, to make it neater.
    //i just moved everything because this cs file is now the system core, everything lives here, instead of in chunks.
    //this also alows for a smaller size because we arent re-using the same code multiple times. the first few commits have theese comments so everybody knows whats going on, they will be removed later.
    //old size = 29 mb, new size = 28.1. it also feels a bit faster when using on a vm.

    class gui
    {
        public static Canvas canvas;
        public static int screenX = 800;
        public static int screenY = 600;

        public static Pen taskbar = new Pen(Color.Purple);
        public static Pen menubtn = new Pen(Color.Orange);

        public static Color backColor = Color.DarkCyan;

        public static void start()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);

            canvas.Clear(backColor);
        }

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(taskbar, 0, screenY - 15, screenX, 15);
        }

        public static void draw_menubtn()
        {
            canvas.DrawFilledRectangle(menubtn, 0, screenY - 15, 15, 15);
        }

        public static bool check_click(int x, int y, int width, int height)
        {
            return mouse.X <= x + width && mouse.X >= x
                && mouse.Y <= y + height && mouse.Y >= y
                    && MouseManager.MouseState == MouseState.Left;
        }

        public static void disable()
        {
            if (Kernel.enabled)
            {
                Kernel.enabled = false;

                canvas.Disable();
                PCScreenFont screenFont = PCScreenFont.Default;
                VGAScreen.SetFont(screenFont.CreateVGAFont(), screenFont.Height);
            }
        }

        public static void enable()
        {
            Console.Clear();

            start();
            mouse.start();

            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                draw_taskbar();
                draw_menubtn();
                draw_mouse();

                if (check_click(0, screenY - 15, 15, 15))
                    disable();
            }
        }

        public static void draw_dialog()
        {

        }

        public static void draw_mouse()
        {
            mouse.draw();
        }
    } //GUI class for drawing objects. contains functions for specific shapes and system popups/pages.

    public class mouse
    {
        private static int lastX, lastY;

        public static int X { get => (int) MouseManager.X; }

        public static int Y { get => (int) MouseManager.Y; }

        private static readonly Pen reset = new Pen(gui.backColor);
        private static readonly Pen pen = new Pen(Color.Black);

        public static void start()
        {
            MouseManager.ScreenWidth = (uint) gui.screenX;
            MouseManager.ScreenHeight = (uint) gui.screenY;
        }

        public static void draw()
        {
            int x = X;
            int y = Y;

            if (reset.Color != gui.backColor)
                reset.Color = gui.backColor;

            gui.canvas.DrawFilledRectangle(reset, lastX, lastY, 10, 10);
            gui.canvas.DrawFilledRectangle(pen, x, y, 10, 10);

            lastX = x;
            lastY = y;
        }
    } //mouse class for GUI

    public class tools
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
    } //rename from Utils, Still same functionality, just moved here so evrything is more tidy.

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
            cmds.Clear();

            AddCommand("print", "Print any string of text, used for console applications.", print);
            AddCommand("about", "About prism OS", about);
            AddCommand("help", "List all available commands", help);
            AddCommand("shutdown", "Shuts down the system.\nArguments\n==========\n-r restarts the system instead", shutdown);
            AddCommand("clear", "clear entire console", clear);
            AddCommand("sysinfo", "Prints system information", sysinfo);
            AddCommand("tone", "used to make sounds in an app", tone);
            AddCommand("list", "List all files in the current directory", list);
            AddCommand("create", "create a file/folder on the hard drive", create);
            AddCommand("gui", "Loads the GUI.", guia);
            AddCommand("keymap", "Change the keyboard layout.\nArguments\n==========\nfr for french layout, us for english layout and de for german layout", keymap);
            AddCommand("dhcp", "Sends a DHCP discover packet.", dhcp);
            AddCommand("ping", "Sends an ICMP packet and returns the elapsed time.\nArguments\n==========\n<address>", ping);
            AddCommand("tcp", "Sends a TCP packet and returns the response body as a UTF-8 string.\nArguments\n==========\n<address> <port> <timeout> <body>", tcp);
        }

        #region Misc Commands

        static void guia(string[] args)
        {
            gui.enable();
        }

        static void keymap(string[] args)
        {
            var layout = args[0];

            if (layout == "fr")
                KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FR_Standard());
            else if (layout == "us")
                KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.US_Standard());
            else if (layout == "de")
                KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DE_Standard());

            Console.WriteLine("Successfully set the keyboard layout to " + layout + "!");
        }

        static void list(string[] args)
        {
            var x = Kernel.fs.GetDirectoryListing(Kernel.root + args);
            Console.WriteLine(x);
        }
        static void create(string[] args)
        {
            var x = Kernel.fs.CreateFile(Kernel.root + args);
            Console.WriteLine("created " + Kernel.root + args);
        }
        static void print(string[] args)
        {
            if (args.Length < 1)
                tools.Error("Insufficient arguments.");

            string content = string.Join(" ", args);
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
                    Console.WriteLine(cmd.Name);

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
            var action = "Shutdown";
            var time = 0;

            if (args.Length > 0)
            {
                if (args[0] == "-r")
                    action = "Reboot";

                if (args[0] == "-t")
                    time = int.Parse(args[1]);
                else if (args.Length > 1 && args[1] == "-t")
                    time = int.Parse(args[2]);
            }

            Console.WriteLine(action + @" machine?
 ____       ____ 
||y ||   / ||n ||
||__||  /  ||__||
|/__\| /   |/__\|"
                );
            ConsoleKeyInfo input = Console.ReadKey(false);

            if (input.KeyChar == 'Y' || input.KeyChar == 'y')
                if (time > 0)
                {
                    tools.Warn(action + " in " + time + " seconds");
                    tools.Sleep(time);

                    if (action == "Shutdown")
                        Cosmos.System.Power.Shutdown();
                    else
                        Cosmos.System.Power.Reboot();
                }
                else
                {
                    if (action == "Shutdown")
                        Cosmos.System.Power.Shutdown();
                    else
                        Cosmos.System.Power.Reboot();
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
            tools.syetem_message("CPU clock speed: " + (cspeed / 1000 / 1000) + " Mhz");
            tools.syetem_message("Total ram: " + ram + " MB");
        }
        static void dhcp(string[] args)
        {
            networking.dhcp();
            tools.syetem_message("Successfully set up DHCP!");
        }
        static void ping(string[] args)
        {
            tools.syetem_message(networking.ping(args[0]).ToString());
        }
        static void tcp(string[] args)
        {
            tools.syetem_message(Encoding.UTF8.GetString(networking.tcp(args[0], int.Parse(args[1]), int.Parse(args[2]), args[3])));
        }
        #endregion
    } //Still same functionality, just moved here so evrything is more tidy.

    public class filesystem
    {
        public static List<DirectoryEntry> lsdir(string args)
        {
            return Kernel.fs.GetDirectoryListing("0:/" + args);
        }

        public static void cdir(string args)
        {
            Kernel.fs.CreateDirectory("0:/" + args);
        }
    } //filesystem class, used for acessing files.

    public class networking
    {
        public static void dhcp()
        {
            using (var client = new DHCPClient())
                client.SendDiscoverPacket();
        }

        public static int ping(string address, int timeout = 5000)
        {
            using (var client = new ICMPClient())
            {
                var point = new EndPoint(Address.Zero, 0);

                client.Connect(Address.Parse(address));
                client.SendEcho();

                return client.Receive(ref point, timeout);
            }
        }

        public static byte[] tcp(string address, int port, int timeout, string body)
        {
            using (var client = new TcpClient(port))
            {
                client.Connect(Address.Parse(address), port);
                client.Send(Encoding.ASCII.GetBytes(body));

                var endpoint = new EndPoint(Address.Zero, 0);

                var data = client.Receive(ref endpoint);
                var data2 = client.NonBlockingReceive(ref endpoint);

                return data2;
            }
        }
    } //networking class, used for acessing the internet. comming soon.
}
