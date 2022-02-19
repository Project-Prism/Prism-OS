using System.Collections.Generic;

namespace PrismOS.Framework.Compilers
{
    public static class Brainfuck
    {
        public static byte[] Compiler(string Input)
        {
            List<byte> Bytes = new();
            int Pointer = 0;
            while (Pointer != Input.Length)
            {
                switch (Input[Pointer++])
                {
                    case '>':
                        Bytes.Add((byte)Process.OPCodes.RAMNxt);
                        break;
                    case '<':
                        Bytes.Add((byte)Process.OPCodes.RAMLst);
                        break;
                    case '+':
                        Bytes.Add((byte)Process.OPCodes.RAMInc);
                        break;
                    case '-':
                        Bytes.Add((byte)Process.OPCodes.RAMDec);
                        break;
                    case '.':
                        Bytes.Add((byte)Process.OPCodes.Write);
                        Bytes.Add(0x0);
                        Bytes.Add(1);
                        break;
                    case ',':
                        Bytes.Add((byte)Process.OPCodes.Read);
                        break;
                    case '$':
                        Bytes.Add((byte)Process.OPCodes.Random);
                        break;
                    case '#':
                        Bytes.Add((byte)Process.OPCodes.RAMJump);
                        Bytes.Add(0x1);
                        Bytes.Add(0);
                        break;
                }
            }

            return Bytes.ToArray();
        }
    }
}