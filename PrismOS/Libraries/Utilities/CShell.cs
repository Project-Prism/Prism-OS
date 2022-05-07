using System.Collections.Generic;
using System.IO;
using System;

namespace PrismOS.Libraries.Utilities
{
    public class CShell
    { // CShell (a.k.a command shell)
        public CShell(string Path)
        {
            this.Path = Path;
            Commands.Add("ls", (string Args) =>
            {
                Args = Args == "" ? Path : Args;
                string Listing = $"Listing for path {(Args.Contains(":\\") ? "" : Path) + Args}\n";
                foreach (string S in Directory.GetDirectories(Args))
                {
                    Listing += $"\nFolder - {S}";
                }
                foreach (string S in Directory.GetFiles(Args))
                {
                    Listing += $"\nFile   - {S}";
                }

                Console.WriteLine(Listing + "\n");
            });
            Commands.Add("mkdir", (string Args) =>
            {
                Directory.CreateDirectory(Args);
            });
            Commands.Add("rmdir", (string Args) =>
            {
                Directory.Delete(Args.Split(" ")[0], Args.EndsWith("-r"));
            });
            Commands.Add("cd", (string Args) =>
            {
                if (Args == ".\\" || Args == ".")
                {
                    return;
                }
                if (Args == "..\\" || Args == "..")
                {
                    if (Path.Length <= 3)
                    {
                        return;
                    }
                    Path = Path[..Path.LastIndexOf("\\")];
                    return;
                }
                if (Args.Contains(":\\"))
                {
                    if (Directory.Exists(Args))
                    {
                        Path = Args;
                    }
                }
                else
                {
                    if (Directory.Exists(Path + Args))
                    {
                        Path += Args + (Args.EndsWith("\\") ? "" : "\\");
                    }
                }
            });
            Commands.Add("cat", (string Args) =>
            {
                Console.WriteLine(File.ReadAllText((Args.Contains(":\\") ? "" : Path) + Args) + "\n");
            });
            Commands.Add("clear", (string Args) =>
            {
                Console.Clear();
            });
            Commands.Add("echo", (string Args) =>
            {
                Console.WriteLine(Args);
            });
        }
        public string Path;
        public Dictionary<string, Action<string>> Commands = new();

        public int Run(string Command)
        {
            return Run(Command, "");
        }
        public int Run(string Command, string Arguments)
        { // Returns 0 if success, returns 1 if command doesn't exist, returns 2 if method fails
            if (Commands.TryGetValue(Command, out var A))
            {
                try
                {
                    A.Invoke(Arguments);
                    return 0;
                }
                catch (Exception EX)
                {
                    Console.WriteLine("Command exception: " + EX.Message + " (Exit code 2)");
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}