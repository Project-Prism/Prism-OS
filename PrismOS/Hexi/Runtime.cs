using System;
using static System.Text.Encoding;
using static PrismOS.Hexi.Utilities;

namespace PrismOS.Hexi
{
    public static class Runtime
    {
        public static void Execute(byte[] ByteCode)
        {
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
                }
            }
        }
    }
}