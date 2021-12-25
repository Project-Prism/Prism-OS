using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PrismOS
{
    /// <summary>
    /// The hexi compiler. read hexi_readme.txt for info on how it works.
    /// </summary>
    public static class HexiCompiler
    {
        public enum Code : byte
        {
            System_Console_Write, // Writes text to the terminal
            System_Console_WriteLine, // Writes text plus a new line to the terminal
        }

        public static Stopwatch ST { get; } = new();

        public static byte[] Compile(string[] Lines)
        {
            ST.Start();
            List<byte> Bytes = new();

            for (int ln = 0; ln < Lines.Length; ln++)
            {
                if (Lines[ln].Contains('(') && Lines[ln].EndsWith(");"))
                {
                    string[] Stage1 = Lines[ln].Replace(");", "").Split('(');
                    string[] Function = Stage1[0].Split(".");
                    string[] Args = SplitCsv(Stage1[1]);

                    switch (Function[0])
                    {
                        case "System":
                            #region System
                            switch (Function[1])
                            {
                                case "Console":
                                    switch (Function[2])
                                    {
                                        case "Write":
                                            Bytes.Add((byte)Code.System_Console_Write);
                                            AddString(ref Bytes, Args[0]);
                                            break;

                                        case "WriteLine":
                                            Bytes.Add((byte)Code.System_Console_WriteLine);
                                            AddString(ref Bytes, Args[0]);
                                            break;
                                    }
                                    break;
                            }
                            break;
                        #endregion System;
                        default:
                            Console.WriteLine("[ERROR] Unknown namespace '" + Function + "'.");
                            break;
                    }
                }
            }

            ST.Stop();
            Console.WriteLine("Finished compiling. [ " + ST.Elapsed.TotalMilliseconds + " milliseconds ]");
            return Bytes.ToArray(); // Done compiling
        }

        public static void Run(byte[] Bytes)
        {
            ST.Restart();
            for (int Index = 0; Index < Bytes.Length; Index++)
            {
                switch (Bytes[Index++])
                {
                    case (byte)Code.System_Console_Write:
                        Console.Write(GetString(Bytes, ref Index));
                        break;

                    case (byte)Code.System_Console_WriteLine:
                        Console.WriteLine(GetString(Bytes, ref Index));
                        break;
                }
            }
            ST.Stop();
            Console.WriteLine("Finished running. [ " + ST.Elapsed.TotalMilliseconds + " milliseconds ]");
        }

        public static string[] SplitCsv(string line)
        {
            List<string> result = new();
            StringBuilder currentStr = new("");
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++) // For each character
            {
                if (line[i] == '\"') { inQuotes = !inQuotes; } // Quotes are closing or opening
                else if (line[i] == ',') // Comma
                {
                    if (!inQuotes) // If not in quotes, end of current string, add it to result
                    {
                        result.Add(currentStr.ToString());
                        currentStr.Clear();
                    }
                    else { currentStr.Append(line[i]); } // If in quotes, just add it }
                }
                else { currentStr.Append(line[i]); } // Add any other character to current string
            }
            result.Add(currentStr.ToString());
            return result.ToArray(); // Return array of all strings
        }

        public static void AddString(ref List<byte> Bytes, string Data)
        {
            Bytes.Add((byte)(Data.Length + 2));
            foreach (char Char in Data)
            {
                Bytes.Add((byte)Char);
            }
        }

        public static string GetString(byte[] Bytes, ref int Index)
        {
            int Count = Bytes[Index++];
            string Str = "";
            for (int i = Index; i < Count; i++)
            {
                Index++;
                Str += (char)Bytes[i];
            }
            return Str;
        }
    }
}