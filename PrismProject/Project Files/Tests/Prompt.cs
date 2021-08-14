using System;
using System.Collections.Generic;
using Console = System.Console;

namespace PrismProject.Software
{
    public class Cmds
    {
        public struct Command
        {
            public string Name, HelpDesc;
            public function func;
        }

        public static List<Command> cmds = new List<Command>();

        public delegate void function(string[] args);

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
            Command cd;
            cd.Name = name;
            cd.HelpDesc = desc;
            cd.func = func;

            cmds.Add(cd);
        }

        public static void Init()
        {
            cmds.Clear();

            AddCommand("about", "About prism OS", About);
            AddCommand("help", "List all available commands", Help);
            //filesystem
            AddCommand("makedir", "create a folder on the selected disk", Make);
            AddCommand("listdir", "List all files in a directory", List);
            AddCommand("read", "Read all text data from a file on the disk", Read);
            AddCommand("write", "Write text to a file.\n    Arguments:\n        PATH: full path to file\n       CONTENTS: data to be written", Write);
            AddCommand("format", "Format a drive to fat32\n    Arguments:\n        Drive id: specify the drive id\n        quick format: true or false", Format);
            //hexi code
            AddCommand("Hexi", "A custom programing language made specificly for Prism OS.", Hexi);
            AddCommand("Color", "Color the terminal!", Setcolor);
        }

        public static void Start()
        {
            Cmds.Init();
            Console.Clear();
            Console.Write("Prism-OS > ");
            string input = Console.ReadLine();
            Cmds.Parse(input);
        }

        private static void Help(string[] args)
        {
            if (args.Length < 1)
            {
                System2.Extentions.Console.WriteColoredLine(
                    "=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n" +
                    "---- List of all available commands ----\n" +
                    "=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n",
                    ConsoleColor.Cyan);

                foreach (Command cmd in cmds)
                    System2.Extentions.Console.WriteColoredLine(cmd.Name, ConsoleColor.Blue);
                Console.WriteLine("\nYou can get more specific help for each command by using: HELP <COMMAND_NAME>\n");
            }
            else
            {
                foreach (Command cmd in cmds)
                {
                    if (args[0] == cmd.Name)
                    {
                        Console.WriteLine(cmd.HelpDesc + "\n");
                        return;
                    }
                }
            }
        }

        private static void About(string[] args)
        {
            System2.Extentions.Console.WriteColoredLine
                (
                    "\nPrism OS (c) 2021, release " + System2.Extentions.Hardware.KernelInfo.Kernel_build + "\n" +
                    "Created by Terminal.cs and deadlocust\n" +
                    "This is a closed beta version of Prism OS, we are not responsible for any damages caused by it.\n",
                    ConsoleColor.DarkMagenta
                );
        }

        private static void List(string[] path)
        {
            Console.WriteLine("List of files for directory " + path);
            foreach (var File in System2.FileSystem.Explore.ListPath(path[0]))
            {
                Console.WriteLine(File);
            }
        }

        private static void Make(string[] path)
        {
            System2.FileSystem.Explore.MakeFile(path[0], path[1]);
        }

        private static void Read(string[] path)
        {
            Console.WriteLine(System2.FileSystem.Explore.Read(path[0]));
        }

        private static void Write(string[] args)
        {
            PrismProject.System2.FileSystem.Explore.Write(args[0], args[1], args[2]);
            Console.Write("Wrote to file ");
            System2.Extentions.Console.WriteColored(args[0], ConsoleColor.Cyan);
            Console.WriteLine(" (" + args[1].Length + "Bytes)");
        }

        private static void Format(string[] args)
        {
            System2.FileSystem.Format.FormatDisk(args[0], Convert.ToBoolean(args[1]));
        }

        private static void Hexi(string[] args)
        {
            //Hexi_Language.Parser.Parse(args[0], true);
            Console.WriteLine("Hexi is not implemented yet!");
        }

        private static void Setcolor(string[] args)
        {
            switch (args.Length)
            {
                case 2:
                    switch (Convert.ToInt32(args[0]))
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;

                        case 1:
                            Console.BackgroundColor = ConsoleColor.White;
                            break;

                        case 3:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            break;

                        case 4:
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            break;

                        case 5:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                    }
                    switch (Convert.ToInt32(args[1]))
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;

                        case 1:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;

                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;

                        case 5:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }
                    break;
            }
        }
    }
}