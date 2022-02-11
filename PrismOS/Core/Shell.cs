using System;
using System.Collections.Generic;
using Cosmos.System.Network.IPv4.UDP.DHCP;

namespace PrismOS.Core
{
    public class Shell : IDisposable
    {
        public Shell()
        {
            Functions = new();
            Functions.Add(new string[] { "print", "Print a text string." }, Print);
            Functions.Add(new string[] { "help", "type 'help' without an argument to get a list of commands. type it with one argument to get help with that one command." }, Help);
            Functions.Add(new string[] { "about", "Gives you information about Prism OS." }, About);
            Functions.Add(new string[] { "shutdown", "Shut down the system" }, ShutDown);
            Functions.Add(new string[] { "reboot", "Reboot the system" }, Reboot);
            Functions.Add(new string[] { "clear", "clears the text screen." }, Clear);
            Functions.Add(new string[] { "color", "sets the console color. [color, foreground color, background color]" }, Color);
            Functions.Add(new string[] { "help", "get gelp for all commands or a specified command." }, Help);

            Console.WriteLine("Prism OS Shell v1 loaded. Type 'help' for a list of commands.");
        }

        public delegate void Method(string[] Params);
        public Dictionary<string[], Method> Functions { get; set; } // Name, Description, Method

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

                foreach (KeyValuePair<string[], Method> Pair in Functions)
                {
                    if (Pair.Key[0] == Command)
                        Pair.Value.Invoke(Args); return;
                }
                Console.WriteLine("Unknown command.");
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.ToString() + ": " + EX.Message);
            }
        }

        private void Print(string[] Params)
        {
            if (Params != null)
                Console.WriteLine(Params);
        }
        private void Help(string[] Params)
        {
            if (Params == null || Params.GetType() != typeof(string))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("________________________________________");
                Console.WriteLine("---- List of all available commands ----");
                Console.WriteLine("=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (KeyValuePair<string[], Method> Pair in Functions)
                {
                    Console.WriteLine("[ " + Pair.Key[0] + " | " + Pair.Key[1] + " ]");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                foreach (KeyValuePair<string[], Method> Pair in Functions)
                {
                    if (Params[0] == Pair.Key[0])
                        Console.WriteLine(Pair.Key[0] + " || " + Pair.Key[1] + " (" + Pair.Value.ToString() + ")");
                }
            }
        }
        private void About(string[] Params)
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
            Console.WriteLine("Prism OS (c) 2022 (22.2.11)");
            Console.WriteLine("Created by Terminal.CS and deadlocust (aka pinkphone)");
            Console.WriteLine();
        }
        private void ShutDown(string[] Params)
        {
            new DHCPClient().SendReleasePacket();
            Cosmos.System.Power.Shutdown();
        }
        private void Reboot(string[] Params)
        {
            new DHCPClient().SendReleasePacket();
            Cosmos.System.Power.Reboot();
        }
        private void Clear(string[] Params)
        {
            Console.Clear();
        }
        private void Color(string[] Params)
        {
            if (Params?.Length == 2 && int.TryParse(Params[0], out int F) && int.TryParse(Params[1], out int B))
            {
                Console.ForegroundColor = (ConsoleColor)F;
                Console.BackgroundColor = (ConsoleColor)B;
            }
            Console.WriteLine("Incorrect paramaters.");
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}