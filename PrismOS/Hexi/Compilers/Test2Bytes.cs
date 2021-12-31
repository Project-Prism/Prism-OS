using System;
using System.Collections.Generic;
using static PrismOS.Hexi.Utilities;

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
                    case "Write":
                        Bytes.Add((byte)Code.Write);
                        Bytes.Add((byte)Arg.Length);
                        foreach (char Char in Arg)
                        {
                            Bytes.Add((byte)Char);
                        }
                        break;
                    case "WriteLine":
                        Bytes.Add((byte)Code.WriteLine);
                        Bytes.Add((byte)Arg.Length);
                        foreach (char Char in Arg)
                        {
                            Bytes.Add((byte)Char);
                        }
                        break;
                    case "jump":
                        Bytes.Add((byte)Code.Jump);
                        Bytes.Add((byte)int.Parse(Arg));
                        break;
                    default:
                        Console.WriteLine("Skipping unknown or unimplemented function '" + Function + "'.");
                        break;
                }
            }

            return Bytes.ToArray();
        }
    }
}