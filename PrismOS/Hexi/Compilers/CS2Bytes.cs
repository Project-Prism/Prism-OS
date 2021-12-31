using System;
using System.Collections.Generic;
using static PrismOS.Hexi.Utilities;

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
                                            Bytes.Add((byte)Code.Write);
                                            AddString(ref Bytes, Args[0]);
                                            break;

                                        case "WriteLine":
                                            Bytes.Add((byte)Code.WriteLine);
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
            return Bytes.ToArray(); // Done compiling
        }
    }
}