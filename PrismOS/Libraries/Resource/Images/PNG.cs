using PrismOS.Libraries.Resource.Compression;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;
using System.IO;

namespace PrismOS.Libraries.Resource.Images
{
    public class PNG : FrameBuffer
    {
        public PNG(byte[] Binary) : base(1, 1)
        {
            BinaryReader reader = new(new MemoryStream(Binary));

            if (!Validate(Binary))
            {
                return;
            }

            while (reader.BaseStream.Position < Binary.Length)
            {
                var len = reader.ReadUInt32();
                long OP = reader.BaseStream.Position;
                var name = reader.ReadString()[^6..^2];
                reader.BaseStream.Position = OP + 4;

                var pos = reader.BaseStream.Position;

                System.Console.Write(name);

                bool done = false;

                switch (name)
                {
                    #region IHDR

                    case "IHDR":
                        Width = reader.ReadUInt32();
                        Height = reader.ReadUInt32();

                        System.Console.WriteLine(Width + "X" + Height);

                        _ = reader.ReadByte(); // bitdepth
                        _ = reader.ReadByte(); // colortype
                        _ = reader.ReadByte(); // compression
                        _ = reader.ReadByte(); // filtermethod
                        _ = reader.ReadByte(); // interlace
                        break;

                    #endregion

                    #region PLTE

                    case "PLTE":
                        break;

                    #endregion

                    #region IDAT

                    case "IDAT":
                        List<byte> buf = new();
                        reader.ReadByte();
                        reader.ReadByte();
                        for (int i = 2; i < len; i++)
                        {
                            buf.Add(reader.ReadByte());
                        }

                        List<byte> data = ZLib.Inflate(buf);

                        BinaryReader d = new(new MemoryStream(data.ToArray()));

                        var totalScanlines = data.Count / (Width + 1) / 4;

                        List<byte> prevScanline = new();

                        for (int y = 0; y < totalScanlines; y++)
                        {
                            var filter = d.ReadByte();

                            List<byte> dat = new();

                            for (int x = 0; x < Width * 4; x++)
                            {
                                dat.Add(d.ReadByte());
                            }
                            List<byte> scanline = new();

                            if (filter == 1)
                            {
                                scanline.Add(dat[0]);
                                for (var index = 1; index < dat.Count; index++)
                                {
                                    scanline.Add((byte)((scanline[index - 4 > 0 ? index - 4 : 0] + dat[index - 1]) % 256));
                                }
                            }
                            if (filter == 2)
                            {
                                for (var index = 0; index < dat.Count; index++)
                                {
                                    scanline.Add((byte)((prevScanline[index] + dat[index]) % 256));
                                }
                            }

                            BinaryReader line = new(new MemoryStream(scanline.ToArray()));
                            prevScanline.Clear();
                            prevScanline.AddRange(scanline);

                            for (int x = 0; x < Width; x++)
                            {
                                var a = line.ReadByte();
                                var r = line.ReadByte();
                                var g = line.ReadByte();
                                var b = line.ReadByte();

                                this[x, y] = (a, r, g, b);
                            }
                        }

                        break;

                    #endregion

                    #region IEND

                    case "IEND":
                        done = true;
                        break;

                    #endregion
                }

                if (done)
                {
                    break;
                }

                reader.BaseStream.Seek((int)(pos + len), new());
                _ = reader.ReadUInt32(); // CRC
            }
        }

        public static byte[] Magic = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        public static bool Validate(byte[] Binary)
        {
            // Not Implemented (Array Range Indexing)
            //return Binary[..7] == Magic;
            return Binary[0] == 137 && Binary[1] == 80 && Binary[2] == 78 && Binary[3] == 71 && Binary[4] == 13 && Binary[5] == 10 && Binary[6] == 26 && Binary[7] == 10;
        }
    }
}