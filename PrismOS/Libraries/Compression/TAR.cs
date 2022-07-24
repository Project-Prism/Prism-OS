using System.IO;

namespace PrismOS.Libraries.Compression
{
    // https://docs.fileformat.com/compression/tar/
    public class TAR
    {
        public const int HeaderSize = 257;
        public char[] LastModifyTime { get; set; }
        public long HRChecksum { get; set; }
        public long OwnerUID { get; set; }
        public long GroupUID { get; set; }
        public char[] LFName { get; set; }
        public char[] Name { get; set; }
        public char[] Size { get; set; }
        public long Mode { get; set; }
        public byte Type { get; set; }

        public static TAR FromBinary(byte[] Binary)
        {
            BinaryReader R = new(new MemoryStream(Binary));
            TAR T = new();

            T.Name = R.ReadChars(100);
            T.Mode = R.ReadInt64();
            T.OwnerUID = R.ReadInt64();
            T.GroupUID = R.ReadInt64();
            T.Size = R.ReadChars(12);
            T.LastModifyTime = R.ReadChars(12);
            T.HRChecksum = R.ReadInt64();
            T.Type = R.ReadByte();
            T.LFName = R.ReadChars(100);

            // Need To Parse Files


            return T;
        }
    }
}
