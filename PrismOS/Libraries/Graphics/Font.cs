using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Graphics
{
    public class Font : IDisposable
    {
        public Font(string Charset, byte[] Binary, uint Size)
        {
            this.Charset = Charset;
            this.Binary = Binary;
            Size16 = Size / 16;
            Size8 = Size / 8;
            Size4 = Size / 4;
            Size2 = Size / 2;
            this.Size = Size;
        }

        public static string DefaultCharset = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public static Font Default = new(DefaultCharset, Assets.Font1B, 16);

        public string Charset;
        public byte[] Binary;
        public uint Size16;
        public uint Size8;
        public uint Size4;
        public uint Size2;
        public uint Size;

        public void Dispose()
        {
            GCImplementation.Free(Binary);
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}