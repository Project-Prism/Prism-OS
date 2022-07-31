using Cosmos.Core;
using System.IO;
using System;

namespace PrismOS.Libraries.Graphics
{
    public class Font : IDisposable
    {
        public Font(string Charset, MemoryStream MS, int Width, int Height)
        {
            this.Charset = Charset;
            this.MS = MS;
            this.Width = Width;
            this.Height = Height;
        }

        public static string DefaultCharset = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public static Font Default = new(DefaultCharset, new(Assets.Font1B), 8, 16);

        public string Charset;
        public MemoryStream MS;
        public int Width;
        public int Height;

        public void Dispose()
        {
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}