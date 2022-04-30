using System.Collections.Generic;
using System.IO;
using System;

namespace PrismOS.Libraries
{
    public static class Hexi
    {
        public static Dictionary<string, byte> Functions = new()
        {
            { "Console.WriteLine", 0x00 },
            { "Console.Write", 0x01 },
            { "Console.Clear", 0x02 },
            { "Console.SetForegroundColor", 0x03 },
            { "Console.SetBackgroundColor", 0x04 },
            { "Console.ResetColor", 0x05 },
            { "Canvas.Clear", 0x06 },
            { "Canvas.SetPixel", 0x07 },
            { "Canvas.Update", 0x08 },
        };

        public static byte[] Compile(string Contents)
        {
            #region Variables

            BinaryWriter Writer = new(new MemoryStream());
            string[] ContentsArray = Contents.Split('\n');

            #endregion

            #region Proccess all lines

            for (int L = 0; L < Contents.Split('\n').Length; L++)
            {
                if (ContentsArray[L].Contains(");") && ContentsArray[L].Contains("("))
                {
                    #region Variables

                    string[] RawData = ContentsArray[L].Replace(");", "").Split("(");
                    string Function = RawData[0];
                    string[] Arguments = RawData[1].Replace(" ", "").Split(",");

                    #endregion

                    // Make sure input function is a correct function
                    if (Functions.ContainsKey(Function))
                    {
                        // Write function byte and argument count
                        Writer.Write(Functions[Function]);

                        // Write arguments as bytes
                        for (int A = 0; A < Arguments.Length; A++)
                        {
                            if (int.TryParse(Arguments[A], out int IntResult))
                            {
                                Writer.Write(IntResult);
                                continue;
                            }
                            Writer.Write(Arguments[A]);
                        }
                    }
                    continue;
                }
            }

            #endregion

            return (Writer.BaseStream as MemoryStream).ToArray();
        }

        public static void Execute(byte[] Binary)
        {
            BinaryReader Reader = new(new MemoryStream(Binary));

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                switch (Reader.ReadByte())
                {
                    case 0x00:
                        Console.WriteLine(Reader.ReadString());
                        break;
                    case 0x01:
                        Console.Write(Reader.ReadString());
                        break;
                    case 0x02:
                        Console.Clear();
                        break;
                    case 0x03:
                        Console.ForegroundColor = (ConsoleColor)Reader.ReadByte();
                        break;
                    case 0x04:
                        Console.BackgroundColor = (ConsoleColor)Reader.ReadByte();
                        break;
                    case 0x05:
                        Console.ResetColor();
                        break;
                    case 0x06:
                        Kernel.Canvas.Clear(new(Reader.ReadInt32()));
                        break;
                    case 0x07:
                        Kernel.Canvas.SetPixel(Reader.ReadInt32(), Reader.ReadInt32(), new(Reader.ReadInt32()));
                        break;
                    case 0x08:
                        Kernel.Canvas.Update();
                        break;
                    default:
                        Console.WriteLine("[ Error ] Unknown command.");
                        break;
                }
            }
        }
    }
}