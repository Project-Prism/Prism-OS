using Color = PrismOS.UI.Common.Color;
using VBE = PrismOS.UI.VBEDriverPlus;
using PrismOS.UI.Components;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using System.Collections.Generic;
using System;

namespace PrismOS.UI
{
    public class Canvas
    {
        public Canvas(int Width, int Height)
        {
            VBE = new((ushort)Width, (ushort)Height, 32);
            Resize(Width, Height);
        }

        #region Variables
        public List<Window> Windows { get; } = new();
        public VBE VBE { get; set; }
        public int[] Buffer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion Variables

        #region Functions
        #region Pixel
        public void SetPixel(int X, int Y, Color Color)
        {
            if (Color.Alpha < 255 && Color.Alpha != 0)
            {
                Color = AlphaBlend(GetPixel(X, Y), Color);
            }
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
            X -= Width / 2;
            Y -= Height / 2;

            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    SetPixel(X + w, Y + h, Color);
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

        public static Color AlphaBlend(Color PixelColor, Color SetColor)
        {
            int R = (SetColor.Red * SetColor.Alpha) + (PixelColor.Red * (255 - SetColor.Alpha)) >> 8;
            int G = (SetColor.Green * SetColor.Alpha) + (PixelColor.Green * (255 - SetColor.Alpha)) >> 8;
            int B = (SetColor.Blue * SetColor.Alpha) + (PixelColor.Blue * (255 - SetColor.Alpha)) >> 8;
            return new(R, G, B);
        }

        public void Update()
        {
            VBE.SetVram(Buffer);
        }

        public void Resize(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new int[Width * Height];
            VBE.VBESet((ushort)Width, (ushort)Height, 32);
            VBE.SetVram(Buffer);
        }
        #endregion Misc
        #endregion Functions
    }
}