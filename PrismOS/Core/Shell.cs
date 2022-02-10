using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.HAL;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace PrismOS.Core
{
    public class Shell : IDisposable
    {
        public Shell()
        {
            Function.AddCommand("print", "Print any string of text, used for console applications.", Print);
            Function.AddCommand("about", "About prism OS", About);
            Function.AddCommand("help", "List all available commands", Help);
            Function.AddCommand("shutdown", "Shuts down the system.\nArguments\n==========\n-r restarts the system instead", Power);
            Function.AddCommand("clear", "clear entire console", Clear);
            Function.AddCommand("write", "path contents", Write);
            Function.AddCommand("svfs", "start the cosmos virtual filesystem", SVFS);
            Function.AddCommand("snet", "Starts the ipv4 networking service.", SNET);
            Function.AddCommand("ls", "List files", LS);
            Console.WriteLine("Shell v1 loaded.");
        }

        public string Path = "0:\\";

        public struct Function
        {
            public delegate void AMethod(string[] Args);
            public Function(string Description, AMethod Method)
            {
                this.Description = Description;
                this.Method = Method;
            }

            public static Dictionary<string, Function> Functions { get; set; } = new();
            public string Description { get; set; }
            public AMethod Method { get; set; }

            public static void AddCommand(string Name, string Description, AMethod Method)
            {
                Functions.Add(Name, new(Description, Method));
            }
        }

        public void SendCommand(string Input)
        {
            try
            {
                string Command;
                string[] Args;
                if (Input.Contains(" "))
                {
                    Command = Input.Split(' ')[0];
                    Args = Input.Split(' ')[1].Split(", ");
                }
                else
                {
                    Command = Input;
                    Args = Array.Empty<string>();
                }

                if (Function.Functions.ContainsKey(Command))
                {
                    Function.Functions[Command].Method(Args);
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
            }
            catch (Exception EX)
            {
                Console.WriteLine("[ ERROR ] " + EX.Message);
            }
        }

        void Print(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }
            string content = string.Join(" ", args);
            Console.WriteLine(content);
        }
        void Help(string[] args)
        {
            if (args.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine("=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (KeyValuePair<string, Function> Pair in Function.Functions)
                {
                    Console.WriteLine("[ " + Pair.Key + " | " + Pair.Value.Description + " ]");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                if (Function.Functions.ContainsKey(args[0]))
                    Console.WriteLine(args[0] + " | " + Function.Functions[args[0]].Description);
            }
        }
        void About(string[] args)
        {
            Console.WriteLine(@"
_______________________________________________
    ____       _                   ____  _____
   / __ \_____(_)________ ___     / __ \/ ___/
  / /_/ / ___/ / ___/ __ `__ \   / / / /\__ \ 
 / ____/ /  / (__  ) / / / / /  / /_/ /___/ / 
/_/   /_/  /_/____/_/ /_/ /_/   \____//____/                                                
_______________________________________________
");
            Console.WriteLine("");
            Console.WriteLine("Prism OS (c) 2021, release 1.2");
            Console.WriteLine("Created by bad-codr and deadlocust");
            Console.WriteLine();
        }
        void Power(string[] args)
        {
            if (args[0] == "-s")
            {
                Console.WriteLine("Shutting down the system will cause all progress to be lost. Continue? [y/n]");
                ConsoleKeyInfo input = Console.ReadKey(false);
                if (input.KeyChar == 'y')
                {
                    DHCPClient DHCPC = new();
                    DHCPC.SendReleasePacket();
                    Cosmos.System.Power.Shutdown();
                }
            }
            else if (args[0] == "-r")
            {
                Console.WriteLine("Restart system? this will cause all progress to be lost [y/n]");
                if (Console.ReadKey(false).KeyChar == 'y')
                {
                    DHCPClient DHCPC = new();
                    DHCPC.SendReleasePacket();
                    Cosmos.System.Power.Reboot();
                }
            }
            else
            {
                Console.WriteLine("Incorrect tack '" + args[0] + "'.");
            }
        }
        void Clear(string[] args)
        {
            Console.Clear();
        }
        void Write(string[] args)
        {
            System.IO.File.WriteAllText(args[0], args[1]);
        }
        void SVFS(string[] args)
        {
            CosmosVFS VFS = new();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }
        void SNET(string[] args)
        {
            IPConfig.Enable(NetworkDevice.GetDeviceByName("eth0"), Address.Zero, Address.Broadcast, Address.Parse("192.168.1.1"));
            DHCPClient DHCPC = new();
            DHCPC.SendDiscoverPacket();
        }
        void LS(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("File entries for path " + Path);
                foreach (string Str in System.IO.Directory.GetFiles(Path))
                {
                    Console.WriteLine(Str);
                }
            }
            else
            {
                if (args[0].Contains(":\\"))
                {
                    Console.WriteLine("File entries for path " + args[0]);
                    foreach (string Str in System.IO.Directory.GetFiles(args[0]))
                    {
                        Console.WriteLine(Str);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect path '" + args[0] + "'.");
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}