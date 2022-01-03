using System;
using System.Collections.Generic;
using System.Text;
using static PrismOS.Hexi.Runtime;

namespace PrismOS.Hexi.Compilers
{
    public static class CS2Bytes
    {
        public static byte[] Compile(string[] Lines)
        {
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
                                            Bytes.Add((byte)Codes.Console.Print);
                                            AddString(ref Bytes, Args[0]);
                                            break;

                                        case "WriteLine":
                                            Bytes.Add((byte)Codes.Console.Print);
                                            AddString(ref Bytes, Args[0] + "\n");
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
            return Bytes.ToArray(); // Done compiling
        }

        internal static void AddString(ref List<byte> Bytes, string String)
        {
            Bytes.Add((byte)String.Length);
            foreach (char Char in String)
            {
                Bytes.Add((byte)Char);
            }
        }

        internal static string[] SplitCsv(string String)
        {
            List<string> result = new();
            StringBuilder currentStr = new("");
            bool inQuotes = false;
            for (int i = 0; i < String.Length; i++) // For each character
            {
                if (String[i] == '\"') // Quotes are closing or opening
                {
                    inQuotes = !inQuotes;
                }
                else if (String[i] == ',') // Comma
                {
                    if (!inQuotes) // If not in quotes, end of current string, add it to result
                    {
                        result.Add(currentStr.ToString());
                        currentStr.Clear();
                    }
                    else
                    {
                        currentStr.Append(String[i]); // If in quotes, just add it 
                    }
                }
                else // Add any other character to current string
                {
                    currentStr.Append(String[i]);
                }
            }
            result.Add(currentStr.ToString());
            return result.ToArray(); // Return array of all strings
        }
    }
}