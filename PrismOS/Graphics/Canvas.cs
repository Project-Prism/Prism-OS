using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using Mouse = Cosmos.System.MouseManager;
using Color = System.Drawing.Color;
using System.Collections.Generic;
using Cosmos.System.Graphics;
using Cosmos.Core;
using System.Text;
using System.IO;
using System;

namespace PrismOS.Graphics
{
    public unsafe class Canvas
    {
        public Canvas(int Width, int Height)
        {
            Resize(Width, Height);
        }

        public List<Mode> Modes = new()
        {
            { new(320, 200, (ColorDepth)32) },
            { new(640, 480, (ColorDepth)32) },
            { new(720, 480, (ColorDepth)32) },
            { new(1024, 768, (ColorDepth)32) },
            { new(1280, 720, (ColorDepth)32) },
            { new(1920, 1080, (ColorDepth)32) }
        };
        public bool ShowMenu;
        private int Selected;
        public VBEDriver VBE;
        public int*[] Buffer;
        private DateTime LT;
        private int Frames;
        public int Height;
        public int Width;
        public int FPS;

        #region Pixel

        public void SetPixel(int X, int Y, Color Color)
        {
            if (X > Width || X < 0 || Y > Height || Y < 0 || Color.A == 0)
                return;
            if (Color.A < 255)
                Color = Blend(GetPixel(X, Y), Color);

            // Draw main pixel
            Buffer[(Width * Y) + X] = (int*)Color.ToArgb();
        }
        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb((int)Buffer[(Width * Y) + X]);
        }
        public static Color Blend(Color Back, Color Front)
        {
            int R = ((Front.A * Front.R) + ((256 - Front.A) * Back.R)) >> 8;
            int G = ((Front.A * Front.G) + ((256 - Front.A) * Back.G)) >> 8;
            int B = ((Front.A * Front.B) + ((256 - Front.A) * Back.B)) >> 8;
            return Color.FromArgb(R, G, B);
        }

        #endregion

        #region Line

        public void DrawLine(int X, int Y, int X2, int Y2, Color Color)
        {
            /*
            int B;
            if (X < X2)
            {
                B = X;
                X = X2;
                X2 = B;
            }
            if (Y < Y2)
            {
                B = Y;
                Y = Y2;
                Y2 = B;
            }

            int I = X2, J = Y2;
            while (I != X2 && J != Y2)
            {
                J++;
                SetPixel(I, J, Color);
                I++;
                SetPixel(I, J, Color);
            }
            */

            int dx = Math.Abs(X2 - X), sx = X < X2 ? 1 : -1;
            int dy = Math.Abs(Y2 - Y), sy = Y < Y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;

            while (X != X2 || Y != Y2)
            {
                SetPixel(X, Y, Color);
                int e2 = err;
                if (e2 > -dx) { err -= dy; X += sx; }
                if (e2 < dy) { err += dx; Y += sy; }
            }
        }
        public void DrawAngledLine(int X, int Y, int Angle, int Radius, Color Color)
        {
            DrawLine(X, Y, (int)(X + (Math.Cos(Angle) * Radius)), (int)(Y + (Math.Sin(Angle) * Radius)), Color);
        }

        #endregion

        #region Rectangle

        public void DrawRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            DrawLine(X, Y, X + Width, Y, Color); // Top Line
            DrawLine(X, Y + Height, X + Width, Height + Y, Color); // Bottom Line
            DrawLine(X, Y, X, Y + Height, Color); // Left Line
            DrawLine(X + Width, Y, Width + X, Y + Height, Color); // Right Line
        }
        public void DrawRectangle(int X, int Y, int Width, int Height, int Radius, Color Color)
        {
            DrawCircle(X, Y, Radius, Color, 180, 270); // Top left
            DrawCircle(X + Width, Y + Height, Radius, Color, 0, 90); // Bottom right
            DrawCircle(X, Y + Height, Radius, Color, 90, 180); // Bottom left
            DrawCircle(X + Width, Y, Radius, Color, 270, 360);

            DrawLine(X + Radius, Y, X + Width - (Radius * 2), Y, Color); // Top Line
            DrawLine(X + Radius, Y + Height, X + Width - (Radius * 2), Height + Y, Color); // Bottom Line
            DrawLine(X, Y + Radius, X, Y + Height - (Radius * 2), Color); // Left Line
            DrawLine(X + Width, Y + Radius, Width + X, Y + Height - (Radius * 2), Color); // Right Line
        }

        public void DrawFilledRectangle(int X, int Y, int Width, int Height, Color Color)
        {
            for (int IY = Y; IY < Y + Height; IY++)
            {
                for (int IX = X; IX < X + Width; IX++)
                {
                    SetPixel(IX, IY, Color);
                }
            }
        }
        public void DrawFilledRectangle(int X, int Y, int Width, int Height, int Radius, Color Color)
        {
            if (Radius == 0)
            {
                DrawFilledRectangle(X, Y, Width, Height, Color);
                return;
            }

            // Draw Outside circles
            DrawFilledCircle(X + Width - (Radius * 2), Y + Height - (Radius * 2), Radius, Color, 0, 90);
            DrawFilledCircle(X - 1, Y + Height - (Radius * 2) - 1, Radius, Color, 270, 360);
            DrawFilledCircle(X + Width - (Radius * 2) + 2, Y - 1, Radius, Color, 90, 180);
            DrawFilledCircle(X - 1, Y, Radius, Color, 180, 270);

            // Draw Main Rectangle
            DrawFilledRectangle(X, Y, Width - (Radius * 2), Height - (Radius * 2), Color);

            // Draw Outside Rectangles
            DrawFilledRectangle(X - Radius, Y, Radius, Height - (Radius * 2), Color);
            DrawFilledRectangle(X + Width - (Radius * 2), Y, Radius, Height - (Radius * 2), Color);
            DrawFilledRectangle(X, Y - Radius, Width - (Radius * 2) + 1, Radius, Color);
            DrawFilledRectangle(X, Y + Width - (Radius * 2), Width - (Radius * 2) + 1, Radius, Color);
            //DrawFilledRectangle(X + Radius, Y, Width - r2, Radius, Color);
            //DrawFilledRectangle(X + Radius, y2 - Radius, Width - r2, Radius, Color);
        }

        #endregion

        #region Circle

        public void DrawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0 || StartAngle == EndAngle)
                return;

            for (; StartAngle < EndAngle; StartAngle++)
            {
                SetPixel(
                    X: (int)(X + (Radius * Math.Sin(Math.PI * StartAngle / 180))),
                    Y: (int)(Y + (Radius * Math.Cos(Math.PI * StartAngle / 180))),
                    Color: Color);
            }
        }

        public void DrawFilledCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0 || StartAngle == EndAngle)
                return;

            for (int I = 0; I < Radius; I++)
            {
                DrawCircle(X, Y, I, Color, StartAngle, EndAngle);
            }
        }

        #endregion

        #region Triangle

        public void DrawTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            DrawLine(X1, Y1, X2, Y2, Color);
            DrawLine(X1, Y1, X3, Y3, Color);
            DrawLine(X2, Y2, X3, Y3, Color);
        }

        #endregion

        #region Image

        public void DrawBitmap(int X, int Y, Bitmap Bitmap)
        {
            for (int IX = 0; IX < Bitmap.Width; IX++)
            {
                for (int IY = 0; IY < Bitmap.Height; IY++)
                    SetPixel(X + IX, Y + IY, Color.FromArgb(Bitmap.rawData[(Bitmap.Width * IY) + IX]));
            }
        }
        public void DrawBitmap(int X, int Y, int Width, int Height, Bitmap Bitmap)
        {
            if (Width == 0 || Height == 0)
                return;

            Bitmap Temp = new((uint)Width, (uint)Height, Cosmos.System.Graphics.ColorDepth.ColorDepth32);
            int x_ratio = (int)((Bitmap.Width << 16) / Width) + 1;
            int y_ratio = (int)((Bitmap.Height << 16) / Height) + 1;
            int x2, y2;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    x2 = (j * x_ratio) >> 16;
                    y2 = (i * y_ratio) >> 16;
                    Temp.rawData[(i * Width) + j] = Bitmap.rawData[(y2 * Bitmap.Width) + x2];
                }
            }
            DrawBitmap(X, Y, Temp);
        }

        #endregion

        #region Text

        public class Font
        {
            public Font(int Width, int Height, string Base64)
            {
                this.Width = Width;
                this.Height = Height;
                MS = new(Convert.FromBase64String(Base64));
            }
            public Font(byte[] Data)
            {
                Width = Data[0];
                Height = Data[1];
                MS = new(Data, 2, Data.Length - 2, true);
            }
            public Font(string FromFile)
            {
                byte[] File = System.IO.File.ReadAllBytes(FromFile);
                Width = File[0];
                Height = File[1];
                MS = new(File, 2, File.Length - 2, true);
            }

            public int Width;
            public int Height;
            public MemoryStream MS;
            public readonly static Font Default = new(8, 16, "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AAB4zMzM2MzGxsbMAAAAAAAA/sbGwMDAwMDAwAAAAAAAAAAA/mxsbGxsbGwAAAAAAAAA/sZgMBgwYMb+AAAAAAAAAAAAftjY2NjYcAAAAAAAAAAAZmZmZmZ8YGDAAAAAAAAAAHbcGBgYGBgYAAAAAAAAAH4YPGZmZjwYfgAAAAAAAAA4bMbG/sbGbDgAAAAAAAA4bMbGxmxsbGzuAAAAAAAAHjAYDD5mZmZmPAAAAAAAAAAAAH7b29t+AAAAAAAAAAAAAwZ+29vzfmDAAAAAAAAAHDBgYHxgYGAwHAAAAAAAAAB8xsbGxsbGxsYAAAAAAAAAAP4AAP4AAP4AAAAAAAAAAAAYGH4YGAAA/wAAAAAAAAAwGAwGDBgwAH4AAAAAAAAADBgwYDAYDAB+AAAAAAAADhsbGBgYGBgYGBgYGBgYGBgYGBgYGNjY2HAAAAAAAAAAABgYAH4AGBgAAAAAAAAAAAAAdtwAdtwAAAAAAAAAOGxsOAAAAAAAAAAAAAAAAAAAAAAAABgYAAAAAAAAAAAAAAAAAAAAGAAAAAAAAAAADwwMDAwM7GxsPBwAAAAAANhsbGxsbAAAAAAAAAAAAABw2DBgyPgAAAAAAAAAAAAAAAAAfHx8fHx8fAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
        }

        public void DrawString(int X, int Y, string Text, Color Color)
        {
            string[] Lines = Text.Split('\n');
            for (int Line = 0; Line < Lines.Length; Line++)
            {
                for (int Char = 0; Char < Lines[Line].Length; Char++)
                {
                    Font.Default.MS.Seek((Encoding.ASCII.GetBytes(Lines[Line][Char].ToString())[0] & 0xFF) * Font.Default.Height, SeekOrigin.Begin);
                    byte[] fontbuf = new byte[Font.Default.Height];
                    Font.Default.MS.Read(fontbuf, 0, fontbuf.Length);

                    for (int IY = 0; IY < Font.Default.Height; IY++)
                    {
                        for (int IX = 0; IX < Font.Default.Width; IX++)
                        {
                            if ((fontbuf[IY] & (0x80 >> IX)) != 0)
                                SetPixel(X + IX + (Char * Font.Default.Width), Y + IY + (Line * Font.Default.Height), Color);
                        }
                    }
                }
            }
        }

        #endregion

        #region Misc

        public void Clear(Color Color = default)
        {
            if (Color == default)
            {
                Color = Color.Black;
            }

            MemoryOperations.Fill((int[])(object)Buffer, Color.ToArgb());
        }

        public void Resize(int Width, int Height)
        {
            VBE.DisableDisplay();
            this.Width = Width;
            this.Height = Height;
            VBE = new((ushort)Width, (ushort)Height, 32);
            Buffer = new int*[Width * Height];
            Mouse.ScreenWidth = (uint)Width;
            Mouse.ScreenHeight = (uint)Height;
            Mouse.X = (uint)Width / 2;
            Mouse.Y = (uint)Height / 2;
        }

        public void Update()
        {
            if (Cosmos.System.KeyboardManager.ControlPressed || ShowMenu)
            {
                ShowMenu = true;
                DrawFilledRectangle(0, 0, 100, Font.Default.Height * Modes.Count, Color.FromArgb(100, 25, 25, 25));
                DrawFilledRectangle(0, Selected * Font.Default.Height, 100, Font.Default.Height, Color.DarkOrange);
                for (int I = 0; I < Modes.Count; I++)
                {
                    DrawString(15, I * Font.Default.Height, Modes[I].Columns + "x" + Modes[I].Rows, Color.White);
                }
                if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
                {
                    if (Key.Key == Cosmos.System.ConsoleKeyEx.UpArrow && Selected != 0)
                    {
                        Selected--;
                    }
                    if (Key.Key == Cosmos.System.ConsoleKeyEx.DownArrow && Selected != Modes.Count - 1)
                    {
                        Selected++;
                    }
                    if (Key.Key == Cosmos.System.ConsoleKeyEx.Enter)
                    {
                        ShowMenu = false;
                        Resize(Modes[Selected].Columns, Modes[Selected].Rows);
                        return;
                    }
                }
            }
            Frames++;
            if ((DateTime.Now - LT).TotalSeconds >= 1)
            {
                FPS = Frames;
                Frames = 0;
                LT = DateTime.Now;
                Cosmos.Core.Memory.Heap.Collect();
            }

            Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy((int[])(object)Buffer);
        }

        #endregion
    }
}