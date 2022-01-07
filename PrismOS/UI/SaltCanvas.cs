using System;
using Color = PrismOS.UI.Common.Color;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using VBE = PrismOS.UI.VBEDriverPlus;

namespace PrismOS.UI
{
    public class SaltCanvas
    {
        public SaltCanvas(int Width, int Height)
        {
            Resize(Width, Height);
        }
        public VBE VBE { get; set; }
        public int[] Buffer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void Update()
        {
            VBE.SetVram(Buffer);
        }
        public void Resize(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            VBE = new((ushort)Width, (ushort)Height, 32);
            Buffer = new int[Width * Height];
            VBE.SetVram(Buffer);
        }

        #region Drawing
        #region Pixel
        public void SetPixel(int X, int Y, Color Color)
        {
            Buffer[X + (Width * Y)] = Color.ARGB;
        }
        public Color GetPixel(int X, int Y)
        {
            return new(Buffer[X + (Width * Y)]);
        }
        #endregion Pixel

        #region Rectangle
        public void DrawFilledRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            for (; X < Width; X++)
            {
                for (;  Y < Height; Y++)
                {
                    SetPixel(X, Y, Color);
                }
            }
        }
        #endregion Rectangle

        #region Image
        public void DrawImage(int X, int Y, Bitmap Bitmap)
        {
            for (; X < Bitmap.Width; X++)
            {
                for (; Y < Bitmap.Height; Y++)
                {
                    SetPixel(X, Y, new(Bitmap.rawData[X * Y * (int)Bitmap.Depth]));
                }
            }
        }
        #endregion Image

        #region Misc
        public void Clear(Color Color)
        {
            Array.Fill(Buffer, Color.ARGB, 0, Buffer.Length);
        }
        #endregion Misc
        #endregion Drawing
    }
}