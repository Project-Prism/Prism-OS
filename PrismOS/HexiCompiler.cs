using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PrismOS
{
    /// <summary>
    /// The hexi compiler. read hexi_readme.txt for info on how it works.
    /// </summary>
    public static class HexiCompiler
    {
        public enum Codes : byte
        {
            NextArg, // Marks the end of an argument
            Print, // Write the following bytes until termination
            Draw, // Draw to the screen
            Halt, // Stop all code execution
            Quit, // Quit running
            DefineDebug, // Define whether the program should launch as debugable
        }

        public static byte[] Compile(string[] Lines)
        {
            Stopwatch ST = new();
            ST.Start();
            List<byte> Bytes = new();
            string[] Args;

            foreach (string Line in Lines)
            {
                if (Line.Length == 0)
                { continue; }
                else if (Line.Contains("#Debug"))
                {
                    Bytes.Add((byte)Codes.DefineDebug);
                }
                else if (Line.Contains("=>"))
                {
                    Args = Line.Split("=>");
                    switch (Args[0])
                    {
                        #region Print
                        case "Print":
                            Bytes.Add((byte)Codes.Print);
                            Bytes.Add((byte)Args[1].Length);
                            foreach (char Char in Args[1])
                            {
                                Bytes.Add((byte)Char);
                            }
                            break;
                        #endregion Print

                        #region Halt
                        case "Halt":
                            Bytes.Add((byte)Codes.Halt);
                            Bytes.Add(0);
                            break;
                        #endregion Halt

                        #region Quit
                        case "Quit":
                            Bytes.Add((byte)Codes.Quit);
                            Bytes.Add(0);
                            break;
                            #endregion Quit
                    }
                }
            }

            ST.Stop();
            Console.WriteLine("Time Elapsed: " + ST.Elapsed.TotalMilliseconds + " MS.");
            return Bytes.ToArray(); // Done compiling
        }

        public static void Run(byte[] Bytes)
        {
            bool IsDebug = false;
            int Index = 0;
            while (true)
            {
                switch (Bytes[Index++])
                {
                    case (byte)Codes.DefineDebug:
                        IsDebug = true;
                        break;

                    case (byte)Codes.Print:
                        int DataLength = Bytes[Index++];
                        for (int i = Index; i < Index + DataLength; i++)
                        {
                            Console.Write((char)Bytes[i]);
                        }
                        Console.Write('\n');
                        break;

                    case (byte)Codes.Halt:
                        Thread.Sleep(-1);
                        break;

                    case (byte)Codes.Quit:
                        if (IsDebug)
                        { Process.GetCurrentProcess().Kill(); }
                        else
                        { Console.WriteLine("The program has exited."); Thread.Sleep(-1); }
                        break;
                }
            }
        }
    }
}