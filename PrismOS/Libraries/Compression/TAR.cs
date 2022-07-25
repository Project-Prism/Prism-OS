using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

namespace PrismOS.Libraries.Compression
{

    // https://docs.fileformat.com/compression/tar/
    public class TAR
    {
        public TAR(byte[] Binary)
        {
            byte[] B = new byte[512];
            for (int I = 512; I < Binary.Length; I += 512)
            {
                for (int IX = 0; IX < 512; IX++)
                {
                    B[IX] = Binary[I + IX];
                }
                //Buffers.Add(new(Binary[I..(I + 512)]));
                Buffers.Add(new(B));
            }
        }

        public struct Buffer
        {
            public Buffer(byte[] Binary)
            {
                BinaryReader R = new(new MemoryStream(Binary));
                Name = Encoding.ASCII.GetString(R.ReadBytes(100)).Trim('\0');
                Mode = R.ReadInt64();
                OwnerUID = R.ReadInt64();
                GroupUID = R.ReadInt64();
                Size = Convert.ToInt64(R.ReadBytes(12).ToString().Trim('\0').Trim());
                LastModifyTime = R.ReadChars(12);
                HRChecksum = R.ReadInt64();
                Type = R.ReadByte();
                LFName = R.ReadChars(100);
            }

            public static int HeaderSize { get => 257; }
            public char[] LastModifyTime { get; set; }
            public long HRChecksum { get; set; }
            public long OwnerUID { get; set; }
            public long GroupUID { get; set; }
            public char[] LFName { get; set; }
            public string Name { get; set; }
            public long Size { get; set; }
            public long Mode { get; set; }
            public byte Type { get; set; }
        }
        public enum Types
        {
            NormalFile = 0,
            HardLink = 1,
            SymbolicLink = 2,
            DeviceOrSpecialFile = 3,
            BlockDevice = 4,
            Firectory = 5,
            NamedPipe = 6,
        }

        public List<Buffer> Buffers { get; set; } = new();
    }
}