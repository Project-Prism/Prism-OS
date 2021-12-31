using System;
using static System.Text.Encoding;
using static PrismOS.Hexi.Utilities;

namespace PrismOS.Hexi
{
    public static class Runtime
    {
        public static void Execute(byte[] ByteCode)
        {
            byte[] Memory = Array.Empty<byte>();

            for (int Index = 0; Index < ByteCode.Length;)
            {
                switch (ByteCode[Index++])
                {
                    #region Write
                    case (byte)Code.Write:
                        Console.Write(UTF8.GetString(ByteCode, Index + 1, ByteCode[Index++]));
                        break;
                    #endregion Write
                    #region WriteLine
                    case (byte)Code.WriteLine:
                        Console.WriteLine(UTF8.GetString(ByteCode, Index + 1, ByteCode[Index++]));
                        break;
                    #endregion WriteLine
                    #region Jump
                    case (byte)Code.Jump:
                        Index = ByteCode[Index];
                        break;
                    #endregion Jump
                    #region Quit
                    case (byte)Code.Quit:
                        return;
                    #endregion Quit
                    #region Allocate
                    case (byte)Code.Allocate:
                        Memory = new byte[ByteCode[Index++]];
                        break;
                    #endregion Allocate
                    #region MemSet
                    case (byte)Code.MemSet:
                        Memory[ByteCode[Index++]] = ByteCode[Index++];
                        break;
                    #endregion MemSet
                }
            }
        }
    }
}