using System.Collections.Generic;
using System.Text;

namespace PrismOS.Hexi.Core
{
    public static class Binary
    {
        public enum OPCodes : byte
        {
            Write =     0x00, // Write to the output string.
            WriteLine = 0x01, // write to the output string, then add a newline.
            Clear =     0x02, // Clear the output string.
            Jump =      0x03, // Jump to a location in the program.
        }

        public static string GetString(byte[] ROM, ref int ROMIndex)
        {
            int ALength = ROM[ROMIndex++];
            ROMIndex += ALength;
            return Encoding.ASCII.GetString(ROM, ROMIndex, ALength);
        }

        public static byte[] GetBytes(byte[] ROM, ref int ROMIndex)
        {
            List<byte> Bytes = new();
            int BytesLength = ROM[ROMIndex++];
            for (; ROMIndex < ROMIndex + BytesLength; ROMIndex++)
            {
                Bytes.Add(ROM[ROMIndex]);
            }
            return Bytes.ToArray();
        }
    }
}