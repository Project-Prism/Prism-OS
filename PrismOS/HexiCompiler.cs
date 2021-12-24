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
            Print, // Write the following bytes until termination
            Sleep, // Sleep for however many milliseconds.
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
                { }
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
                            string Str = Args[1].Replace("\\n", "\n");
                            Bytes.Add((byte)Str.Length);
                            foreach (char Char in Str)
                            {
                                Bytes.Add((byte)Char);
                            }
                            break;
                        #endregion Print

                        #region Sleep
                        case "Sleep":
                            Bytes.Add((byte)Codes.Sleep);
                            int.Parse(Args[1]);
                            Bytes.Add((byte)Args[1].Length);
                            break;
                        #endregion Sleep

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
                switch (Bytes[Index])
                {
                    case (byte)Codes.DefineDebug:
                        Index++;
                        IsDebug = true;
                        break;

                    case (byte)Codes.Print:
                        Index++;
                        int DataLength = Bytes[Index++];
                        for (int i = Index; i < Index + DataLength; i++)
                        {
                            Console.Write((char)Bytes[i]);
                        }
                        break;

                    case (byte)Codes.Sleep:
                        if (IsDebug) { Console.WriteLine("Sleeping for " + (int)Bytes[Index++]); }
                        Thread.Sleep(Bytes[Index++]);
                        break;

                    case (byte)Codes.Quit:
                        Index++;
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