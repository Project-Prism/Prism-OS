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

    public class Gui
    {
        public static Canvas canvas;
        public static int screenX = 800;
        public static int screenY = 600;

        public static Pen taskbar = new Pen(Color.DarkSlateGray);
        public static Pen menubtn = new Pen(Color.Red);
        public static Pen main_theme = new Pen(Color.White);
        public static Pen main_theme_title = new Pen(Color.DarkSlateGray);

        public static Color backColor = Color.CornflowerBlue;

        public static void start()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);

            canvas.Clear(backColor);
        }

        public static void draw_taskbar()
        {
            canvas.DrawFilledRectangle(taskbar, 0, screenY - 30, screenX, 30);
        }

        public static void draw_menubtn()
        {
            //canvas.DrawFilledRectangle(menubtn, 0, screenY - 30, 30, 30);
            canvas.DrawFilledCircle(menubtn, 20, screenY - 15, 10);
        }

        public static bool check_click(int x, int y, int width, int height)
        {
            return Mouse.X <= x + width && Mouse.X >= x
                && Mouse.Y <= y + height && Mouse.Y >= y
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
            Mouse.start();
            
            //only update when the mouse moves, needs to be changed in the future, but should work for now.
            Kernel.enabled = true;
            while (Kernel.enabled)
            {
                int oldx = Mouse.X;
                if(oldx != Mouse.X)
                {
                    draw_dialog();
                    draw_taskbar();
                    draw_menubtn();
                    draw_mouse();

                    if (check_click(0, screenY - 30, 30, 30))
                    Cosmos.System.Power.Reboot();
                }
            }
            
                
            }

        public static void draw_dialog()
        {
            canvas.DrawFilledRectangle(main_theme, screenX / 4, screenY / 4, screenX / 4, screenY / 4);
            canvas.DrawFilledRectangle(main_theme_title, screenX / 4, screenY / 4, screenX / 4, screenY / 20);
        }

        public static void draw_mouse()
        {
            Mouse.draw();
        }
    }

    public class Mouse
    {
        private static int lastX, lastY;

        public static int X { get => (int) MouseManager.X; }

        public static int Y { get => (int) MouseManager.Y; }

        private static readonly Pen reset = new Pen(Gui.backColor);
        private static readonly Pen pen = new Pen(Color.White);

        public static void start()
        {
            MouseManager.ScreenWidth = (uint) Gui.screenX;
            MouseManager.ScreenHeight = (uint) Gui.screenY;
        }

        public static void draw()
        {
            int x = X;
            int y = Y;

            if (reset.Color != Gui.backColor)
                reset.Color = Gui.backColor;

            Gui.canvas.DrawFilledRectangle(reset, lastX, lastY, 10, 10);
            Gui.canvas.DrawFilledRectangle(pen, x, y, 10, 10);
            //Gui.canvas.DrawFilledCircle(reset, lastX, lastY, 10);
            //Gui.canvas.DrawFilledCircle(pen, x, y, 10);

            lastX = x;
            lastY = y;
        }
    }

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

            Tools.Error("Invalid command.");
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
            Gui.enable();
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
                Tools.Error("Insufficient arguments.");

            string content = string.Join(" ", args);
            Console.WriteLine(content);
        }
        static void tone(string[] args)
        {
            Tools.argcheck(args, 2, 2);
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
            Console.WriteLine(@"
_______________________________________________
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/                                                
_______________________________________________
");
            Tools.SetColor(ConsoleColor.Green);
            Console.WriteLine("");
            Console.WriteLine("Prism OS (c) 2021, release " + Kernel.versionID);
            Console.WriteLine("Created by bad-codr and deadlocust");
            Tools.Warn("This is a closed beta version of Prism OS, we are not responsible for any damages caused by it.");
            Console.WriteLine();
            Tools.SetColor(ConsoleColor.White);
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
                    Tools.Warn(action + " in " + time + " seconds");
                    Tools.Sleep(time);

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
            Tools.syetem_message("CPU clock speed: " + (cspeed / 1000 / 1000) + " Mhz");
            Tools.syetem_message("Total ram: " + ram + " MB");
        }
        static void dhcp(string[] args)
        {
            Networking.dhcp();
            Tools.syetem_message("Successfully set up DHCP!");
        }
        static void ping(string[] args)
        {
            Tools.syetem_message(Networking.ping(args[0]).ToString());
        }
        static void tcp(string[] args)
        {
            Tools.syetem_message(Encoding.UTF8.GetString(Networking.tcp(args[0], int.Parse(args[1]), int.Parse(args[2]), args[3])));
        }
        #endregion
    }

    public class Filesystem
    {
        public static List<DirectoryEntry> lsdir(string args)
        {
            return Kernel.fs.GetDirectoryListing("0:/" + args);
        }

        public static void cdir(string args)
        {
            Kernel.fs.CreateDirectory("0:/" + args);
        }
    }

    public class Networking
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
    }
}
