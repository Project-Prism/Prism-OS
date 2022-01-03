using System;
using System.Collections.Generic;
using static PrismOS.Hexi.Runtime;

namespace PrismOS.Hexi.Compilers
{
    public static class Test2Bytes
    {
        public static byte[] Compile(string[] Lines)
        {
            List<byte> Bytes = new();

            foreach (string Line in Lines)
            {
                string Function = Line.Split("=>")[0];
                string Arg = Line.Split("=>")[1];

                Console.WriteLine("Function '" + Function + "' with args '" + Arg + "'.");

                switch (Function)
                {
                    case "SetPixel":
                        Bytes.Add((byte)Codes.Graphics.SetPixel);
                        string[] Args = Arg.Split(", ");
                        Bytes.Add((byte)int.Parse(Args[0]));
                        Bytes.Add((byte)int.Parse(Args[1]));
                        Bytes.Add((byte)int.Parse(Args[2]));
                        break;
                    default:
                        Console.WriteLine("Skipping unknown or unimplemented function '" + Function + "'.");
                        break;
                }
            }

            return Bytes.ToArray();
        }

        internal static void AddString(ref List<byte> Bytes, string String)
        {
            Bytes.Add((byte)String.Length);
            foreach (char Char in String)
            {
                Bytes.Add((byte)Char);
            }
        }
    }
}