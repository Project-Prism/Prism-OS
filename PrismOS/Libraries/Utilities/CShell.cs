using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Utilities
{
    public unsafe static class CommandManager
    {
        public static List<Command> Commands = new()
        {
            new()
            {
                Name = "Test1",
                Description = "A command to test the functionality of the command manager",
                Permission = "User",
                Usage = "[ Empty ]",
                Target = () => { Console.WriteLine("Testing 1"); },
            },
        };

        public class Command
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Usage { get; set; }
            public string Permission { get; set; }
            public Action Target { get; set; }
        }
    }
}