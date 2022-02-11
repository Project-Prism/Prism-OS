using PrismOS.Generic;
using System.Collections.Generic;

namespace PrismOS.Network.NWeb
{
    public class NWebCommand
    {
        public NWebCommand(byte Method, string Path)
        {
            this.Method = Method;
            this.Path = Path;
        }

        public NWebCommand(byte Method, string Path, string Data)
        {
            this.Method = Method;
            this.Path = Path;
            this.Data = Data;
        }

        public byte Method { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }

        public byte[] ToBytes()
        {
            List<byte> Bytes = new();
            Bytes.Add(Method);
            switch (Method)
            {
                case 0x0: // Get
                    Bytes.Add((byte)Path.Length);
                    for (int I = 0; I < Path.Length; I++)
                    {
                        Bytes.Add((byte)Path[I]);
                    }
                    break;
                case 0x1: // Post
                    Bytes.Add((byte)Path.Length);
                    for (int I = 0; I < Path.Length; I++)
                    {
                        Bytes.Add((byte)Path[I]);
                    }

                    Bytes.Add((byte)Data.Length);
                    for (int I = 0; I < Data.Length; I++)
                    {
                        Bytes.Add((byte)Data[I]);
                    }
                    break;
            }
            return Bytes.ToArray();
        }

        public static NWebCommand FromBytes(byte[] Data)
        {
            return Data[0] switch
            {
                // Get
                0x0 => new(
                                Data[0],
                                System.Text.Encoding.UTF8.GetString(Data, 2, Data[3])),
                _ => null,
            };
        }
    }
}