using System;
using System.Collections.Generic;
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


            AddCommand("about", "About prism OS", about);
            AddCommand("help", "List all available commands", help);
            //filesystem
            AddCommand("makedir", "create a folder on the selected disk", Create_Directory);
            AddCommand("listdir", "List all files in a directory", List_Directory);
            AddCommand("read", "Read all text data from a file on the disk", Read_file);
            AddCommand("write", "Write text to a file.\n    Arguments:\n        PATH: full path to file\n       CONTENTS: data to be written", Write_file);
            AddCommand("format", "Format a drive to fat32\n    Arguments:\n        Drive id: specify the drive id\n        quick format: true or false", format);
            //graphics
            AddCommand("gui", "Loads the GUI.", Gui);
        }

        #region Misc Commands
        static void Gui(string[] args)
        {
            Desktop.Start();
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
        #endregion
    }
}
