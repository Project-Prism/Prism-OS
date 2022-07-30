using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

namespace PrismOS.Libraries.Compression
{
    // https://docs.fileformat.com/compression/tar/
    public unsafe class TAR
    {
        public struct Header
        {
            public Header(byte[] Binary)
            {
                BinaryReader R = new(new MemoryStream(Binary));
                Name = Encoding.ASCII.GetString(R.ReadBytes(100)).Trim('\0');
                Mode = R.ReadInt64();
                OwnerUID = R.ReadInt64();
                GroupUID = R.ReadInt64();
                Size = Convert.ToInt64(Encoding.UTF8.GetString(R.ReadBytes(12)).Trim('\0').Trim(), 8);
                LastModifyTime = R.ReadChars(12);
                HRChecksum = R.ReadInt64();
                Type = (FileTypes)R.ReadByte();
                LFName = R.ReadChars(100);
            }

            public enum FileTypes
            {
                NormalFile = 0,
                HardLink = 1,
                SymbolicLink = 2,
                DeviceOrSpecialFile = 3,
                BlockDevice = 4,
                Directory = 5,
                NamedPipe = 6,
            }

            public static int HeaderSize { get => 512; }
            public char[] LastModifyTime { get; set; }
            public long HRChecksum { get; set; }
            public long OwnerUID { get; set; }
            public long GroupUID { get; set; }
            public char[] LFName { get; set; }
            public string Name { get; set; }
            public long Size { get; set; }
            public long Mode { get; set; }
            public FileTypes Type { get; set; }
        }
        public TAR(byte[] Binary)
        {
            BinaryReader R = new(new MemoryStream(Binary));

            while (R.BaseStream.Position < Binary.Length + 512)
            {
                Header H = new(R.ReadBytes(512));
                Console.WriteLine("Adding " + H.Name + ", Length of " + H.Size);
                Blocks.Add(H.Name, R.ReadBytes((int)H.Size));
                R.BaseStream.Position += H.Size;
            }
        }

        public Dictionary<string, byte[]> Blocks { get; set; } = new();
    }
}