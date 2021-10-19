using Cosmos.System.FileSystem.Listing;
using System;
using static PrismProject.Filesystem.FSCore;
using Cosmos.System.Network.IPv4;

namespace PrismProject._System
{
    class Terminal
    {
        public static void Main()
        {
            Display.Visual2D.DisplayConfig.Controler.Disable();
            Console.Clear();
            Console.WriteLine("Terminal.cs, V1");
            while (true)
            {
                Console.Write("> ");
                string[] input = Console.ReadLine().Split("<>");
                switch (input[0])
                {
                    case "fm":
                        switch (input[1])
                        {
                            case "create":
                                switch (input[2])
                                {
                                    case "folder":
                                        CreateFolder(input[3]);
                                        Console.WriteLine("[ Done ]");
                                        break;
                                    case "file":
                                        WriteFile(input[3], "", false);
                                        Console.WriteLine("[ Done ]");
                                        break;
                                }
                                break;
                            case "delete":
                                DeleteEntry(input[2]);
                                Console.WriteLine("[ Done ]");
                                break;
                            case "write":
                                WriteFile(input[2], input[3], false);
                                Console.WriteLine("[ Done ]");
                                break;
                            case "echofile":
                                Console.WriteLine(ReadFile(input[2]));
                                Console.WriteLine("[ Done ]");
                                break;
                            case "list":
                                foreach (DirectoryEntry item in GetFolderList(input[2]))
                                {
                                    Console.WriteLine(item.mName);
                                }
                                break;
                            case "mkfs":
                                Console.Write("This will erase all data and write a new fs base. Continue? (Y/N) > ");
                                string a = Console.ReadLine();
                                switch (a.ToLower())
                                {
                                    case "y":
                                        Services.FTSE.MakeFs();
                                        break;
                                    case "n":
                                        break;
                                }
                                break;
                        }
                        break;
                    case "system":
                        switch (input[1])
                        {
                            case "shutdown":
                                Cosmos.System.Power.Shutdown();
                                break;
                            case "reboot":
                                Cosmos.System.Power.Reboot();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("[!] No command");
                        break;
                } // Cmd looks like: fm(write (the action), contents here, 0:\path\to\file);
            }
        }
    }
}