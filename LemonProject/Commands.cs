using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemonProject
{
    public class Cmds
    {
        public static List<string> cmds = new List<string>();

        public static void Parse(string input)
        {
            string[] args = input.Split(new char[0]);
            string[] cmdargs = input.Remove(0, input.IndexOf(' ') + 1).Split(new char[0]);
            if (!cmds.Contains(args[0]))
            {
                Utils.Error("Invalid command.");
            }
            if (args[0].Equals("print")) { print(cmdargs); }
            if (args[0].Equals("credits")) { credits(); }
            if (args[0].Equals("help")) { help(cmdargs); }
        }

        public static void Init()
        {
            cmds.Add("print");
            cmds.Add("credits");
            cmds.Add("help");
        }

        static void print(string[] args)
        {
            string content = String.Join(" ", args);
            Console.WriteLine(content);
        }
        
        static void help(string[] args)
        {
            if (args.Length < 1)
            {
                Utils.colorCache = Console.ForegroundColor;
                SetColor(ConsoleColor.Yellow);
                Console.WriteLine("--Commands--");
                SetColor(ConsoleColor.DarkYellow);
                foreach (string cmd in cmds)
                {
                    Console.WriteLine(cmd);
                }
                SetColor(colorCache);
                Console.WriteLine("You can get more specific help for each command by using: HELP <COMMAND_NAME>");
                Console.WriteLine();
            }
            else
            {
                // todo: add specific help for each command by checking args
            }
        }
        
        static void credits()
        {
            Console.WriteLine("Lemon OS (c) 2021");
            Console.WriteLine("By bad-codr and deadlocust");
            Console.WriteLine();
        }
    }
} 
