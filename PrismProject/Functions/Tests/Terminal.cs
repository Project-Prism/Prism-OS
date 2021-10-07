using Cosmos.System.FileSystem.Listing;
using System;
using static PrismProject.Functions.IO.Disk;
using static PrismProject.Functions.Network.Basic;

namespace PrismProject.Functions.Tests
{
    class Terminal
    {
        public static void Main()
        {
            Console.Clear();
            
            //Network.DebugServer.TCPServer();
            Console.WriteLine("Terminal.cs, V1");
            while (true)
            {
                //Network.DebugServer.Send("It worked!");
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
                    case "net":
                        Console.WriteLine("Starting network...");
                        NetStart(Local, Subnet, Gateway1);
                        Console.WriteLine("Connecting...");
                        Networking.BasicTCPClient.TCPClient("127.0.0.1", 2323, "Conected?\n");
                        break;
                    default:
                        Console.WriteLine("[!] No command");
                        //Network.DebugServer.Send("no cmd");
                        break;
                }
            }
        }
    }
}