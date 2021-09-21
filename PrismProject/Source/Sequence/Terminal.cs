using Cosmos.System.FileSystem.Listing;
using System;
using static PrismProject.Source.FileSystem.Disk;
using static PrismProject.Source.Network.Interface;

namespace PrismProject.Source.Sequence
{
    class Terminal
    {
        public static void Main()
        {
            Start();
            Console.Clear();
            Console.WriteLine("Started disk service.");
            Console.WriteLine("Terminal.cs, V1");
            while (true)
            {
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(".");
                switch (input[0])
                {
                    case "fm":
                        switch (input[1].ToLower())
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
                        }
                        break;
                    case "power":
                        switch (input[1].ToLower())
                        {
                            case "shutdown":
                                Cosmos.System.Power.Shutdown();
                                break;
                            case "reboot":
                                Cosmos.System.Power.Reboot();
                                break;
                        }
                        break;
                    case "inet":
                        switch (input[1].ToLower())
                        {
                            case "init":
                                NetStart(Local, Subnet, Gateway1);
                                Console.WriteLine("[ Done ]");
                                break;
                            case "connect":
                                Console.WriteLine("Not implemented.");
                                break;
                        }
                        break;
                    case "gui":
                        switch (input[1].ToLower())
                        {
                            case "start":
                                Boot.Main();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("[!] No command");
                        break;
                }
            }
        }
    }
}
