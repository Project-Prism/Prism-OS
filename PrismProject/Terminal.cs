using Cosmos.System.FileSystem.Listing;
using System;
using static PrismProject.Core.FileSystem;
using static PrismProject.Services.Network_Service;
using static PrismProject.Core.TCP_Client;
using Cosmos.System.Network.IPv4;

namespace PrismProject
{
    class Terminal
    {
        public static void Main()
        {
            NetStart(Local, Subnet, Gateway1);
            StartDisk();
            Console.Clear();
            Console.WriteLine("Terminal.cs, V1");
            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                string input = line.Split("(")[0];
                string[] Args = line.Replace(");", "").Split("(")[1].Split(", ");
                switch (input)
                {
                    case "fm":
                        switch (Args[0])
                        {
                            case "create":
                                switch (Args[1])
                                {
                                    case "folder":
                                        CreateFolder(Args[2]);
                                        Console.WriteLine("[ Done ]");
                                        break;
                                    case "file":
                                        WriteFile(Args[2], "", false);
                                        Console.WriteLine("[ Done ]");
                                        break;
                                }
                                break;
                            case "delete":
                                DeleteEntry(Args[1]);
                                Console.WriteLine("[ Done ]");
                                break;
                            case "write":
                                WriteFile(Args[1], Args[2], false);
                                Console.WriteLine("[ Done ]");
                                break;
                            case "echofile":
                                Console.WriteLine(ReadFile(Args[1]));
                                Console.WriteLine("[ Done ]");
                                break;
                            case "list":
                                foreach (DirectoryEntry item in GetFolderList(Args[1]))
                                {
                                    Console.WriteLine(item.mName);
                                }
                                break;
                        }
                        break;
                    case "system":
                        switch (Args[0])
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
                        TCPC(new Address(127, 0, 0, 1), 2323);
                        break;
                    case "dl":
                        Download(Args[0], 80, @"0:\index.html");
                        break;
                    default:
                        Console.WriteLine("[!] No command");
                        break;
                } // Cmd looks like: fm(write (the action), contents here, 0:\path\to\file);
            }
        }
    }
}