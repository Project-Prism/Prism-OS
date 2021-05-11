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
            if (args[0] == "print")
            {
                print(cmdargs);
            }
        }

        public static void Init()
        {
            cmds.Add("print");
        }

        static void print(string[] args)
        {
            string content = String.Join(" ", args);
            Console.WriteLine(content);
        }
        
        static void credits(string[] args)
        {
            Console.WriteLine("Lemon OS (c) 2021");
            Console.WriteLine("By bad-codr and deadlocust");
        }
    }
} 
