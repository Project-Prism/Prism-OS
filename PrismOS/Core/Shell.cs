using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;

namespace PrismOS.Core
{
    public struct Function
    {
        public delegate void AMethod(string[] Args);
        public Function(string Name, string Description, AMethod Method)
        {
            this.Name = Name;
            this.Description = Description;
            this.Method = Method;
        }

        public static List<Function> Functions { get; set; } = new();
        public string Name { get; set; }
        public string Description { get; set; }
        public AMethod Method { get; set; }

        public static void AddCommand(string Name, string Description, AMethod Method)
        {
            Functions.Add(new(Name, Description, Method));
        }
    }

    public class Shell : IDisposable
    {
        public Shell()
        {
            Function.AddCommand("print", "Print any string of text, used for console applications.", Print);
            Function.AddCommand("about", "About prism OS", About);
            Function.AddCommand("help", "List all available commands", Help);
            Function.AddCommand("shutdown", "Shuts down the system.\nArguments\n==========\n-r restarts the system instead", Power);
            Function.AddCommand("clear", "clear entire console", Clear);
            Function.AddCommand("hexi", "(compile onputFile outputFile) or (run file)", Hexi);
            Function.AddCommand("write", "path contents", Write);
            Function.AddCommand("svfs", "start the cosmos virtual filesystem", SVFS);
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

                foreach (Function Function in Function.Functions)
                {
                    if (Command == Function.Name)
                    {
                        Function.Method(Args);
                        return;
                    }
                }
                Console.WriteLine("Unknown command.");
            }
            catch (Exception EX)
            {
                Console.WriteLine("Error: "+ EX.Message);
            }
        }

        void Hexi(string[] args)
        {
            switch (args[0])
            {
                case "compile":
                    Core.Hexi.Main.Compiler.Compile(args[1], args[2]);
                    break;
                case "run":
                    Core.Hexi.Main.Runtime.RunProgram(args[1]);
                    break;
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
                foreach (Function Function in Function.Functions)
                {
                    Console.WriteLine("[ " + Function.Name + " ]");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
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
                    Cosmos.System.Power.Shutdown();
                }
            }
            else if (args[0] == "-r")
            {
                Console.WriteLine("Restart system? this will cause all progress to be lost [y/n]");
                if (Console.ReadKey(false).KeyChar == 'y')
                {
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
        void Calculate(string[] args)
        {
            // move math class here
        }
        void SVFS(string[] args)
        {
            CosmosVFS VFS = new();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public void Dispose()
        {
            // need gc
            //GC.SuppressFinalize(this);
            //GC.Collect();
        }
    }
}