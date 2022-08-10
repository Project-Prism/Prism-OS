using Cosmos.Core;
using System;

namespace PrismOS.Libraries.Graphics
{
    public unsafe class Font : IDisposable
    {
        public Font(string Charset, byte[] Binary, uint Size)
        {
            this.Charset = Charset;
            fixed (byte* PTR = Binary)
            {
                this.Binary = PTR;
            }
            Size16 = Size / 16;
            Size8 = Size / 8;
            Size4 = Size / 4;
            Size2 = Size / 2;
            this.Size = Size;
        }

        public static string DefaultCharset = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public static Font Default = new(DefaultCharset, Assets.Font1B, 16);

        public string Charset;
        public byte* Binary;
        public uint Size16;
        public uint Size8;
        public uint Size4;
        public uint Size2;
        public uint Size;

        private uint DrawChar(int X, int Y, char Char, bool Center)
        {
            uint Index = (uint)Charset.IndexOf(Char);
            if (Char == ' ')
            {
                return Size2;
            }
            if (Char == '\t')
            {
                return Size2 * 4;
            }
            if (Index < 0)
            {
                return Size2;
            }
            if (Center)
            {
                X -= (int)Size16;
                Y -= (int)Size8;
            }

            uint MaxX = 0;
            uint SizePerFont = Size * Size8 * Index;

            for (int h = 0; h < Size; h++)
            {
                for (int aw = 0; aw < Size8; aw++)
                {
                    for (int ww = 0; ww < 8; ww++)
                    {
                        if ((Binary[SizePerFont + (h * Size8) + aw] & (0x80 >> ww)) != 0)
                        {
                            int N = (aw * 8) + ww;
                            if (N > MaxX)
                            {
                                MaxX = (uint)N;
                            }
                        }
                    }
                }
            }
            return MaxX;
        }

        public uint MeasureString(string String)
        {
            uint Width = 0;
            for (int I = 0; I < String.Length; I++)
            {
                Width += DrawChar(0, 0, String[I], false) + 2;
            }
            return Width;
        }

        public void Dispose()
        {
            Cosmos.Core.Memory.Heap.Free(Binary);
            GCImplementation.Free(Charset);
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}