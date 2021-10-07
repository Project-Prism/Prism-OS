using Cosmos.System.FileSystem.Listing;
using System;
using static PrismProject.Functions.IO.Disk;
using static PrismProject.Functions.Network.Basic;
using Cosmos.System.Network.IPv4;

namespace PrismProject.Functions.System2
{
    class Terminal
    {
        public static void Main()
        {
            NetStart(Local, Subnet, Gateway1);
            IO.Disk.StartDisk();
            Console.Clear();
            Console.WriteLine("Terminal.cs, V1");
            while (true)
            {
                Console.Write("> ");
                string[] input = Console.ReadLine().Split(" ");
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
                    case "system":
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
                    case "net":
                        TCPClient(new Address(127, 0, 0, 1), 2323);
                        break;
                    case "dl":
                        Download("http://www.github.com/index.html", 80, @"0:\index.html");
                        break;
                    default:
                        Console.WriteLine("[!] No command");
                        break;
                }
            }
        }
    }
}