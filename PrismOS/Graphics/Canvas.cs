using System;
using Color = System.Drawing.Color;
using Bitmap = Cosmos.System.Graphics.Bitmap;
using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using Cosmos.System.Graphics.Fonts;
using PrismOS.Common;
using System.Numerics;

namespace PrismOS.Graphics
{
    public class Canvas : IDisposable
    {
        public Canvas(int Width, int Height)
        {
            VBE = new((ushort)Width, (ushort)Height, 32);
            Buffer = new int[Width * Height];
            this.Height = Height;
            this.Width = Width;
        }

        public float FFar = 100.0f;
        public float FNear = 0.1f;
        public float FOV = 90.0f;
        public VBEDriver VBE;
        public int[] Buffer;
        public int Height;
        public int Width;

        #region Pixel

        public void SetPixel(int X, int Y, Color Color)
        {
            if (X > Width || X < 0 || Y > Height || Y < 0 || Color.A == 0)
            {
                return;
            }
            if (Color.A < 255)
            {
                Color = Blend(GetPixel(X, Y), Color);
            }

            // Draw main pixel
            Buffer[(Width * Y) + X] = Color.ToArgb();
        }
        public Color GetPixel(int X, int Y)
        {
            return Color.FromArgb(Buffer[(Width * Y) + X]);
        }
        public static Color Blend(Color Back, Color Front) => Color.FromArgb(
        (Back.R * (1 - Front.A)) + Front.R,
        (Back.G * (1 - Front.A)) + Front.G,
        (Back.B * (1 - Front.A)) + Front.B);

        #endregion

        #region Line

        public void DrawLine(int X, int Y, int X2, int Y2, Color Color)
        {
            int C;
            bool steep = false;
            if (Math.Abs(X - X2) < Math.Abs(Y - Y2))
            {
                C = X;
                X = Y;
                Y = C;

                C = X2;
                X2 = Y2;
                Y2 = C;

                steep = true;
            }
            if (X > X2)
            {
                C = X;
                X = X2;
                X2 = C;

                C = Y;
                Y = Y2;
                Y2 = C;
            }
            int dx = X2 - X;
            int dy = Y2 - Y;
            float derror = Math.Abs(dy / (float)dx);
            float error = 0;
            int y = Y;
            for (int x = X; x <= X2; x++)
            {
                if (steep)
                {
                    SetPixel(y, x, Color);
                }
                else
                {
                    SetPixel(x, y, Color);
                }
                error += derror;
                if (error > .5)
                {
                    y += (Y2 > Y ? 1 : -1);
                    error--;
                }
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
            int x2 = X + Width, y2 = Y + Height, r2 = Radius + Radius;
            // Draw Outside circles
            DrawFilledCircle(X + Radius, Y + Radius, Radius, Color);
            DrawFilledCircle(x2 - Radius - 1, Y + Radius, Radius, Color);
            DrawFilledCircle(X + Radius, y2 - Radius - 1, Radius, Color);
            DrawFilledCircle(x2 - Radius - 1, y2 - Radius - 1, Radius, Color);

            // Draw Main Rectangle
            DrawFilledRectangle(X, Y + Radius, Width, Height - r2, Color);
            // Draw Outside Rectangles
            DrawFilledRectangle(X + Radius, Y, Width - r2, Radius, Color);
            DrawFilledRectangle(X + Radius, y2 - Radius, Width - r2, Radius, Color);
        }

        #endregion

        #region Circle

        public void DrawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
            {
                return;
            }
            for (double I = StartAngle; I < EndAngle; I += 0.05)
            {
                SetPixel(X + (Radius * (int)Math.Cos(I)), Y + (Radius * (int)Math.Sin(I)), Color);
            }
        }
        public void DrawFilledCircle(int X, int Y, int Radius, Color Color)
        {
            if (Radius == 0)
            {
                return;
            }

            int y = 0;
            int xChange = 1 - (Radius << 1);
            int yChange = 0;
            int radiusError = 0;

            while (Radius >= y)
            {
                for (int I = X - Radius; I <= X + Radius; I++)
                {
                    SetPixel(I, Y + y, Color);
                    SetPixel(I, Y - y, Color);
                }
                for (int I = X - y; I <= X + y; I++)
                {
                    SetPixel(I, Y + Radius, Color);
                    SetPixel(I, Y - Radius, Color);
                }

                y++;
                radiusError += yChange;
                yChange += 2;
                if (((radiusError << 1) + xChange) > 0)
                {
                    Radius--;
                    radiusError += xChange;
                    xChange += 2;
                }
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
                {
                    SetPixel(X + IX, Y + IY, Color.FromArgb(Bitmap.rawData[(Bitmap.Width * IY) + IX]));
                }
            }
        }
        public void DrawBitmap(int X, int Y, int Width, int Height, Bitmap Bitmap)
        {
            Bitmap Temp = new((uint)Width, (uint)Height, Bitmap.Depth);

            int XR = (((int)Bitmap.Width << 16) / Width) + 1;
            int YR = (((int)Bitmap.Height << 16) / Height) + 1;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int x2 = (j * XR) >> 16;
                    int y2 = (i * YR) >> 16;
                    Temp.rawData[(i * Width) + j] = Bitmap.rawData[(y2 * Bitmap.Width) + x2];
                }
            }
            DrawBitmap(X, Y, Temp);
        }

        #endregion

        #region Text

        public void DrawChar(int X, int Y, char Char, Color Color, PCScreenFont Font)
        {
            int p = Font.Height * (byte)Char;

            for (int cy = 0; cy < Font.Height; cy++)
            {
                for (byte cx = 0; cx < Font.Width; cx++)
                {
                    if (Font.ConvertByteToBitAddres(Font.Data[p + cy], cx + 1))
                    {
                        SetPixel(X + (Font.Width - cx), Y + cy, Color);
                    }
                }
            }
        }
        public void DrawString(int X, int Y, string String, Color Color, PCScreenFont Font = default)
        {
            if (Font == default)
            {
                Font = PCScreenFont.Default;
            }

            foreach (char Char in String)
            {
                DrawChar(X, Y, Char, Color, Font);
                X += Font.Width;
            }
        }

        #endregion

        #region 3D

        public void DrawCube(float Scale, Color Color)
        {
            Mesh Cube;
            Cube.Triangles = new Triangle[]
            {
                // South
                new(0.0f, 0.0f, 0.0f,    0.0f, 1.0f, 0.0f,    1.0f, 1.0f, 0.0f),
                new(0.0f, 0.0f, 0.0f,    1.0f, 1.0f, 0.0f,    1.0f, 0.0f, 0.0f),

                // East
                new(1.0f, 0.0f, 0.0f,    1.0f, 1.0f, 0.0f,    1.0f, 1.0f, 1.0f),
                new(1.0f, 0.0f, 0.0f,    1.0f, 1.0f, 1.0f,    1.0f, 0.0f, 1.0f),

                // North
                new(1.0f, 0.0f, 1.0f,    1.0f, 1.0f, 1.0f,    0.0f, 1.0f, 1.0f),
                new(1.0f, 0.0f, 1.0f,    0.0f, 1.0f, 1.0f,    0.0f, 0.0f, 1.0f),

                // West
                new(0.0f, 0.0f, 1.0f,    0.0f, 1.0f, 1.0f,    0.0f, 1.0f, 0.0f),
                new(0.0f, 0.0f, 1.0f,    0.0f, 1.0f, 0.0f,    0.0f, 0.0f, 0.0f),

                // Top
                new(0.0f, 1.0f, 0.0f,    0.0f, 1.0f, 1.0f,    1.0f, 1.0f, 1.0f),
                new(0.0f, 1.0f, 0.0f,    1.0f, 1.0f, 1.0f,    1.0f, 1.0f, 0.0f),

                // Bottom
                new(1.0f, 0.0f, 1.0f,    0.0f, 0.0f, 1.0f,    0.0f, 0.0f, 0.0f),
                new(1.0f, 0.0f, 1.0f,    0.0f, 0.0f, 0.0f,    1.0f, 0.0f, 0.0f)
                };
            Matrix4x4 Matrix = new();
            float AspectRatio = Height / Width;
            float FOVRad = 1.0f / (float)Math.Tan(FOV  * 0.5f / 180.0f * 3.14159f);

            Matrix.M11 = AspectRatio * FOVRad;
            Matrix.M22 = FOVRad;
            Matrix.M33 = FFar / (FFar - FNear);
            Matrix.M43 = -FFar * FNear / (FFar * FNear);
            Matrix.M34 = 1.0f;
            Matrix.M44 = 0.0f;

            for (int I = 0; I < Cube.Triangles.Length; I++)
            {
                Triangle TriProjected = new(), TriTranslated;

                TriTranslated = Cube.Triangles[I];
                TriTranslated.V1.Z = Cube.Triangles[I].V1.Z + 3.0f;
                TriTranslated.V2.Z = Cube.Triangles[I].V2.Z + 3.0f;
                TriTranslated.V3.Z = Cube.Triangles[I].V3.Z + 3.0f;

                MultiplyMatrixVector(ref TriTranslated.V1, ref TriProjected.V1, ref Matrix);
                MultiplyMatrixVector(ref TriTranslated.V2, ref TriProjected.V2, ref Matrix);
                MultiplyMatrixVector(ref TriTranslated.V3, ref TriProjected.V3, ref Matrix);

                TriProjected.V1.X += Scale; TriProjected.V1.Y += Scale;
                TriProjected.V2.X += Scale; TriProjected.V2.Y += Scale;
                TriProjected.V3.X += Scale; TriProjected.V3.Y += Scale;

                TriProjected.V1.X *= 0.5f * Width; TriProjected.V1.Y *= 0.5f * Height;
                TriProjected.V2.X *= 0.5f * Width; TriProjected.V2.Y *= 0.5f * Height;
                TriProjected.V3.X *= 0.5f * Width; TriProjected.V3.Y *= 0.5f * Height;

                DrawTriangle((int)TriProjected.V1.X, (int)TriProjected.V1.Y, (int)TriProjected.V2.X, (int)TriProjected.V2.Y, (int)TriProjected.V3.X, (int)TriProjected.V3.Y, Color);
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
            Array.Fill(Buffer, Color.ToArgb());
        }

        public void Update()
        {
            if (Buffer.Length != Width * Height)
            {
                Buffer = new int[Width * Height];
                VBE.VBESet((ushort)Width, (ushort)Height, 32);
                return;
            }
            Cosmos.Core.Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy(Buffer, 0, Buffer.Length);
        }

        public static void MultiplyMatrixVector(ref Vector3 i, ref Vector3 o, ref Matrix4x4 m)
        {
            o.X = (i.X * m.M11) + (i.Y * m.M21) + (i.Z * m.M31) + m.M41;
            o.Y = (i.X * m.M12) + (i.Y * m.M22) + (i.Z * m.M32) + m.M42;
            o.Z = (i.X * m.M13) + (i.Y * m.M23) + (i.Z * m.M33) + m.M43;
            float w = (i.X * m.M14) + (i.Y * m.M24) + (i.Z * m.M34) + m.M44;

            if (w != 0.0f)
            {
                o.X /= w; o.Y /= w; o.Z /= w;
            }
        }

        public void Dispose()
        {
            VBE.DisableDisplay();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}