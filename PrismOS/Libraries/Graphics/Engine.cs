using PrismOS.Libraries.Graphics.Types.Shapes;
using PrismOS.Libraries.Graphics.Types;
using PrismOS.Libraries.Numerics;
using System.Collections.Generic;
using System;

namespace PrismOS.Libraries.Graphics
{
    public class Engine
    {
        // To-Do: Implement Camera Rotation
        public Engine(uint Width, uint Height, int FOV)
        {
            Buffer = new(Width, Height);
            this.Width = Width;
            this.Height = Height;
            Objects = new();
            Camera = new();
            this.FOV = FOV;
        }

        public double Z0 => Width / 2 / Math.Tan(FOV / 2 * 3.14159265 / 180);
        public List<Shape> Objects;
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

        public void Render(FrameBuffer Canvas)
        {
            Buffer.Clear();

            // Calculate Objects
            for (int O = 0; O < Objects.Count; O++)
            {
                // Temporary Object Values
                Triangle[] DrawTriangles = Objects[0].Triangles.ToArray();

                // Calculate Object
                for (int I = 0; I < Objects[O].Triangles.Count; I++)
                {
                    // Rotate
                    DrawTriangles[I] = new Triangle()
                    {
                        P1 = Rotate(DrawTriangles[I].P1, Objects[O].Rotation),
                        P2 = Rotate(DrawTriangles[I].P2, Objects[O].Rotation),
                        P3 = Rotate(DrawTriangles[I].P3, Objects[O].Rotation),
                    };

                    // Translate
                    DrawTriangles[I] = new Triangle()
                    {
                        P1 = Translate(DrawTriangles[I].P1, Objects[O].Position + Camera.Position),
                        P2 = Translate(DrawTriangles[I].P2, Objects[O].Position + Camera.Position),
                        P3 = Translate(DrawTriangles[I].P3, Objects[O].Position + Camera.Position),
                    };

                    // Perspective
                    DrawTriangles[I] = new Triangle()
                    {
                        P1 = ApplyPerspective(DrawTriangles[I].P1, Z0),
                        P2 = ApplyPerspective(DrawTriangles[I].P2, Z0),
                        P3 = ApplyPerspective(DrawTriangles[I].P3, Z0),
                    };
                    
                    // Center
                    DrawTriangles[I] = new Triangle()
                    {
                        P1 = Center(DrawTriangles[I].P1, Width, Height),
                        P2 = Center(DrawTriangles[I].P2, Width, Height),
                        P3 = Center(DrawTriangles[I].P3, Width, Height),
                    };
                }

                // Draw Object
                foreach (Triangle T in DrawTriangles)
                {
                    if (T.NormalZ < 0)
                    {
                        Buffer.DrawTriangle((int)T.P1.X, (int)T.P1.Y, (int)T.P2.X, (int)T.P2.Y, (int)T.P3.X, (int)T.P3.Y, Color.White);
                    }
                }
            }

            Canvas.DrawImage(0, 0, Buffer, false);
        }

        public static Vector3 ApplyPerspective(Vector3 Original, double Z0)
        {
            return new Vector3
            (
                Original.X * Z0 / (Z0 + Original.Z),
                Original.Y * Z0 / (Z0 + Original.Z),
                Original.Z,
                Original.U * Z0 / (Z0 + Original.Z),
                Original.V * Z0 / (Z0 + Original.Z),
                Original.W * Z0 / (Z0 + Original.Z)
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

            toReturn.X = Original.X * (Math.Cos(Rotation.Z) * Math.Cos(Rotation.Y)) + Original.Y * (Math.Cos(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Sin(Rotation.X) - Math.Sin(Rotation.Z) * Math.Cos(Rotation.X)) + Original.Z * (Math.Cos(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Cos(Rotation.X) + Math.Sin(Rotation.Z) * Math.Sin(Rotation.X));
            toReturn.Y = Original.X * (Math.Sin(Rotation.Z) * Math.Cos(Rotation.Y)) + Original.Y * (Math.Sin(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Sin(Rotation.X) + Math.Cos(Rotation.Z) * Math.Cos(Rotation.X)) + Original.Z * (Math.Sin(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Cos(Rotation.X) - Math.Cos(Rotation.Z) * Math.Sin(Rotation.X));
            toReturn.Z = Original.X * (-Math.Sin(Rotation.Y)) + Original.Y * (Math.Cos(Rotation.Y) * Math.Sin(Rotation.X)) + Original.Z * (Math.Cos(Rotation.Y) * Math.Cos(Rotation.X));
            toReturn.U = Original.U;
            toReturn.V = Original.V;
            toReturn.W = Original.W;
            return toReturn;
        }
    }
}