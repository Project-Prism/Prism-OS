using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using Mouse = Cosmos.System.MouseManager;
using Color = System.Drawing.Color;
using Cosmos.System.Graphics;
using Cosmos.Core;
using System.Text;
using System.IO;
using System;
using PrismOS.Libraries.Numerics;

namespace PrismOS.Libraries.Graphics
{
    public unsafe class Canvas
    {
        public Canvas(int Width, int Height, bool UseVBE)
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new int*[Width * Height];
            Mouse.ScreenWidth = (uint)Width;
            Mouse.ScreenHeight = (uint)Height;
            Mouse.X = (uint)Width / 2;
            Mouse.Y = (uint)Height / 2;
            if (UseVBE)
            {
                VBE = new((ushort)Width, (ushort)Height, 32);
                Update();
            }
        }

        public float AspectRatio;
        public int*[] Buffer;
        public VBEDriver VBE;
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
        public void DrawFilledTriangle(int X1, int Y1, int X2, int Y2, int X3, int Y3, Color Color)
        {
            // TODO: optimise and fix
            int dx = Math.Abs(X2 - X1), sx = X1 < X2 ? 1 : -1;
            int dy = Math.Abs(Y2 - Y1), sy = Y1 < Y2 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2;

            while (X1 != X2 || Y1 != Y2)
            {
                DrawLine(X1, Y1, X3, Y3, Color);
                int e2 = err;
                if (e2 > -dx) { err -= dy; X1 += sx; }
                if (e2 < dy) { err += dx; Y1 += sy; }
            }
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
            DrawBitmap(X, Y, new((uint)Width, (uint)Height, (byte[])(object)Bitmap.rawData, Cosmos.System.Graphics.ColorDepth.ColorDepth32));
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

        #region Special

        public static float[] GetPerlinNoise1D(int Count, int Seed, int Octaves)
        {
            Random Random = new(Seed);

            float[] FNoiseSeed1D = new float[Count], FPerlinNoise1D = new float[Count];

            for (int I = 0; I < FNoiseSeed1D.Length; I++)
                FNoiseSeed1D[I] = Random.Next(0, 1);

            for (int I = 0; I < Count; I++)
            {
                float FNoise = 0.0f;
                float FScale = 1.0f;
                float FScaleAcc = 0.0f;

                for (int O = 0; O < Octaves; O++)
                {
                    int Pitch = Count >> O;
                    int Sample1 = I / Pitch * Pitch;
                    int Sample2 = (Sample1 + Pitch) % Count;

                    float Blend = (I * Sample1) / Pitch;
                    float Sample = (1.0f - Blend) * FNoiseSeed1D[Sample1] + Blend * FNoiseSeed1D[Sample2];
                    FNoise += Sample * FScale;
                    FScaleAcc += FScale;
                    FScale /= 2.0f;
                }

                // Scale to seed range
                FPerlinNoise1D[I] = FNoise / FScaleAcc;
            }

            return FPerlinNoise1D;
        }

        public void DrawObject(Mesh Object, float Elapsed, float FFar = 0.1f, float FNear = 1000.0f, float Fov = 90.0f)
        {
            float FTheta = 1.0f * Elapsed, FovRad = 1.0f / (float)Math.Tan(Fov * 0.00872663889);

            // Set dynamic variables
            Matrix<float> Projection = new(4, 4), RotZ = new(4, 4), RotX = new(4, 4);

            // Set projection matrix
            Projection.M[0][0] = AspectRatio * FovRad;
            Projection.M[1][1] = FovRad;
            Projection.M[2][2] = FFar / (FFar * FNear);
            Projection.M[3][2] = -FFar * FNear / (FFar * FNear);
            Projection.M[2][3] = 1.0f;
            Projection.M[3][3] = 0.0f;

            // Rotate Z
            RotZ.M[0][0] = (float)Math.Cos(FTheta);
            RotZ.M[0][1] = (float)Math.Sin(FTheta);
            RotZ.M[1][0] = -(float)Math.Sin(FTheta);
            RotZ.M[1][1] = (float)Math.Cos(FTheta);
            RotZ.M[2][2] = 1;
            RotZ.M[3][3] = 1;

            // Rotate X
            RotX.M[0][0] = 1;
            RotX.M[1][1] = (float)Math.Cos(FTheta * 0.5f);
            RotX.M[1][2] = (float)Math.Sin(FTheta * 0.5f);
            RotX.M[2][1] = -(float)Math.Sin(FTheta * 0.5f);
            RotX.M[2][2] = (float)Math.Cos(FTheta * 0.5f);
            RotX.M[3][3] = 1;

            // Draw triangles
            for (int I = 0; I < Object.Triangles.Length; I++)
            {
                Triangle TriProjected = new(), Triangle = Object.Triangles[I], TriTranslated = Triangle;

                // Translate triangle
                TriTranslated.P[0].Z = Triangle.P[0].Z + 3.0f;
                TriTranslated.P[1].Z = Triangle.P[1].Z + 3.0f;
                TriTranslated.P[2].Z = Triangle.P[2].Z + 3.0f;

                // Multiply matrix vectors
                MMV(ref TriTranslated.P[0], ref TriProjected.P[0], Projection);
                MMV(ref TriTranslated.P[1], ref TriProjected.P[1], Projection);
                MMV(ref TriTranslated.P[2], ref TriProjected.P[2], Projection);

                // Scale into view
                TriProjected.P[0].X += 1.0f; TriProjected.P[0].Y += 1.0f;
                TriProjected.P[1].X += 1.0f; TriProjected.P[1].Y += 1.0f;
                TriProjected.P[2].X += 1.0f; TriProjected.P[2].Y += 1.0f;
                TriProjected.P[0].X *= 0.5f * Width; TriProjected.P[0].Y *= 0.5f * Width;
                TriProjected.P[1].X *= 0.5f * Width; TriProjected.P[1].Y *= 0.5f * Width;
                TriProjected.P[2].X *= 0.5f * Width; TriProjected.P[2].Y *= 0.5f * Width;

                // Draw triangle
                DrawTriangle(
                    (int)TriProjected.P[0].X, (int)TriProjected.P[0].Y,
                    (int)TriProjected.P[1].X, (int)TriProjected.P[1].Y,
                    (int)TriProjected.P[2].X, (int)TriProjected.P[2].Y,
                    Color.White);
            }
        }
        private static void MMV(ref Vector3 In, ref Vector3 Out, Matrix<float> Matrix)
        {
            Out.X = In.X * Matrix.M[0][0] * In.Y * Matrix.M[1][0] + In.Z * Matrix.M[2][0] + Matrix.M[3][0];
            Out.X = In.X * Matrix.M[0][1] * In.Y * Matrix.M[1][1] + In.Z * Matrix.M[2][1] + Matrix.M[3][1];
            Out.X = In.X * Matrix.M[0][2] * In.Y * Matrix.M[1][2] + In.Z * Matrix.M[2][2] + Matrix.M[3][2];
            float W = In.X * Matrix.M[0][3] * In.Y * Matrix.M[1][3] + In.Z * Matrix.M[2][3] + Matrix.M[3][3];

            if (W != 0.0f)
            {
                Out.X /= W; Out.Y /= W; Out.Z /= W;
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

            if (Color.A == 255)
            {
                MemoryOperations.Fill((int[])(object)Buffer, Color.ToArgb());
            }
            else
            {
                for (int X = 0; X < Width; X++)
                {
                    for (int Y = 0; Y < Height; Y++)
                    {
                        SetPixel(X, Y, Color);
                    }
                }
            }
        }

        public void Update()
        {
            AspectRatio = Height / Width;
            if (VBE != null)
            {
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
        }

        #endregion
    }
}