using PrismOS.Libraries.Rasterizer.Objects;
using PrismOS.Libraries.Rasterizer.Types;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Numerics;
using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Rasterizer
{
    public class Engine : IDisposable
    {
        // To-Do: Implement Camera Rotation
        public Engine(uint Width, uint Height, int FOV)
        {
            this.Width = Width;
            this.Height = Height;
            Objects = new();
            Camera = new();
            this.FOV = FOV;
        }

        #region Engine Data

        public double Gravity = 1.0;
        public List<Mesh> Objects;
        public FrameBuffer Buffer;
        public Camera Camera;
        public int FOV;

        public uint Width
        {
            get
            {
                return Buffer.Width;
            }
            set
            {
                if (Buffer.Width != value)
                {
                    Buffer = new(value, Height);
                }
            }
        }
        public uint Height
        {
            get
            {
                return Buffer.Height;
            }
            set
            {
                if (Buffer.Height != value)
                {
                    Buffer = new(Width, value);
                }
            }
        }

        #endregion

        public void Render(FrameBuffer Canvas)
        {
            double Z0 = Width / 2 / Math.Tan(FOV / 2 * 0.0174532925); // 0.0174532925 == pi / 180

            Buffer.Clear();

            // Calculate Objects
            for (int O = 0; O < Objects.Count; O++)
            {
                #region Physics

                if (Objects[O].HasPhysics)
                {
                    Objects[O].Step(Gravity, 1.0);
                }

                #endregion

                #region Temporary Values

                Triangle[] DrawTriangles = Objects[O].Triangles.ToArray();

                #endregion

                #region Loop Triangles

                for (int T = 0; T < Objects[O].Triangles.Count; T++)
                {
                    #region Rotate

                    DrawTriangles[T] = new Triangle()
                    {
                        P1 = Rotate(DrawTriangles[T].P1, Objects[O].Rotation),
                        P2 = Rotate(DrawTriangles[T].P2, Objects[O].Rotation),
                        P3 = Rotate(DrawTriangles[T].P3, Objects[O].Rotation),
                        T1 = DrawTriangles[T].T1,
                        T2 = DrawTriangles[T].T2,
                        T3 = DrawTriangles[T].T3,
                        Color = DrawTriangles[T].Color,
                    };

                    #endregion

                    #region Translate

                    DrawTriangles[T] = new Triangle()
                    {
                        P1 = Translate(DrawTriangles[T].P1, Objects[O].Position + Camera.Position),
                        P2 = Translate(DrawTriangles[T].P2, Objects[O].Position + Camera.Position),
                        P3 = Translate(DrawTriangles[T].P3, Objects[O].Position + Camera.Position),
                        T1 = DrawTriangles[T].T1,
                        T2 = DrawTriangles[T].T2,
                        T3 = DrawTriangles[T].T3,
                        Color = DrawTriangles[T].Color,
                    };

                    #endregion

                    #region Perspective

                    DrawTriangles[T] = new Triangle()
                    {
                        P1 = ApplyPerspective(DrawTriangles[T].P1, Z0),
                        P2 = ApplyPerspective(DrawTriangles[T].P2, Z0),
                        P3 = ApplyPerspective(DrawTriangles[T].P3, Z0),
                        T1 = DrawTriangles[T].T1,
                        T2 = DrawTriangles[T].T2,
                        T3 = DrawTriangles[T].T3,
                        Color = DrawTriangles[T].Color,
                    };

                    #endregion

                    #region Center

                    DrawTriangles[T] = new Triangle()
                    {
                        P1 = Center(DrawTriangles[T].P1, Width, Height),
                        P2 = Center(DrawTriangles[T].P2, Width, Height),
                        P3 = Center(DrawTriangles[T].P3, Width, Height),
                        T1 = DrawTriangles[T].T1,
                        T2 = DrawTriangles[T].T2,
                        T3 = DrawTriangles[T].T3,
                        Color = DrawTriangles[T].Color,
                    };

                    #endregion

                    #region Normalize

                    double Normal =
                        (DrawTriangles[T].P2.X - DrawTriangles[T].P1.X) *
                        (DrawTriangles[T].P3.Y - DrawTriangles[T].P1.Y) -
                        (DrawTriangles[T].P2.Y - DrawTriangles[T].P1.Y) *
                        (DrawTriangles[T].P3.X - DrawTriangles[T].P1.X);

                    #endregion

                    #region Culling

                    if (Normal < 0)
                    {

                        #region Draw Triangle

                        Buffer.DrawFilledTriangle(
                            (int)DrawTriangles[T].P1.X, (int)DrawTriangles[T].P1.Y,
                            (int)DrawTriangles[T].P2.X, (int)DrawTriangles[T].P2.Y,
                            (int)DrawTriangles[T].P3.X, (int)DrawTriangles[T].P3.Y,
                            DrawTriangles[T].Color);

                        #endregion
                    }

                    #endregion
                }

                #endregion

                #region Memory Managment

                Cosmos.Core.GCImplementation.Free(DrawTriangles);

                #endregion
            }

            // Draw Buffer
            Canvas.DrawImage(0, 0, Buffer, false);
        }

        public static Vector3 ApplyPerspective(Vector3 Original, double Z0)
        {
            double Cache = Z0 / (Z0 + Original.Z);
            return new Vector3
            (
                Original.X * Cache,
                Original.Y * Cache,
                Original.Z,
                Original.U * Cache,
                Original.V * Cache,
                Original.W * Cache
            );
        }
        public static Vector3 Translate(Vector3 Original, Vector3 Translated)
        {
            return new Vector3
            (
                Original.X + Translated.X,
                Original.Y + Translated.Y,
                Original.Z + Translated.Z
            );
        }
        public static Vector3 Center(Vector3 Original, uint Width, uint Height)
        {
            return new Vector3
            (
                Original.X + (Width / 2),
                Original.Y + (Height / 2),
                Original.Z
            );
        }
        public static Vector3 Rotate(Vector3 Original, Vector3 Rotation)
        {
            Vector3 toReturn = new();

            double CosRotX = Math.Cos(Rotation.X);
            double CosRotY = Math.Cos(Rotation.Y);
            double CosRotZ = Math.Cos(Rotation.Z);
            double SinRotX = Math.Sin(Rotation.X);
            double SinRotY = Math.Sin(Rotation.Y);
            double SinRotZ = Math.Sin(Rotation.Z);

            toReturn.X = Original.X * (CosRotZ * CosRotY) + Original.Y * (CosRotZ * SinRotY * SinRotX - SinRotZ * CosRotX) + Original.Z * (CosRotZ * SinRotY * CosRotX + SinRotZ * SinRotX);
            toReturn.Y = Original.X * (SinRotZ * CosRotY) + Original.Y * (SinRotZ * SinRotY * SinRotX + CosRotZ * CosRotX) + Original.Z * (SinRotZ * SinRotY * CosRotX - CosRotZ * SinRotX);
            toReturn.Z = Original.X * (-SinRotY) + Original.Y * (CosRotY * SinRotX) + Original.Z * (CosRotY * CosRotX);
            toReturn.U = Original.U;
            toReturn.V = Original.V;
            toReturn.W = Original.W;
            return toReturn;
        }

        public void Dispose()
        {
            Cosmos.Core.GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}