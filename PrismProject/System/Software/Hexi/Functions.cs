using System;
using System.Collections.Generic;
using System.Text;

namespace PrismProject
{
    class Functions
    {
        public static void consoleout(dynamic[] args)
        {
            if (args.Length > 1)
            {
                switch (args[1])
                {
                    case true:
                        Console.WriteLine(args[0]);
                        break;
                    case false:
                        Console.Write(args[0]);
                        break;
                }
            }
            else
            {
                Console.WriteLine(args[0]);
            }
        }
    }
}
