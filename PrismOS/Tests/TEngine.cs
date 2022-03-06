using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Numerics;
using System;
using System.Drawing;

namespace PrismOS.Tests
{
    public class TEngine
    {
        public TEngine(Canvas Canvas)
        {
            this.Canvas = Canvas;
        }

        public static class Shapes
        {
            public abstract class Shape
            {
                public Vector3 Rotation, Position;
                public Vector3[] Points;
            }

            public class Cube : Shape
            {
                public Cube()
                {
                    Position = new(0, 0, 0);
                    Rotation = new(0, 0, 400);
                    Points = new Vector3[]
                    {
                        new(-200, -200, -200),
                        new(-200, 200, -200),
                        new(200, 200, -200),
                        new(200, -200, -200),
                        new(-200, -200, 200),
                        new(-200, 200, 200),
                        new(200, 200, 200),
                        new(200, -200, 200),
                    };
                }
            }
        }
        public Canvas Canvas;
        public float Z0, FOV = 90.0f;

        public Shapes.Cube TestCube = new();

        public void OnUpdate(float Elapsed = 1.0f)
        {
            Z0 = Canvas.Width / 2.0f / (float)(Math.Tan(FOV / 2.0f * 3.14159265 / 180.0f));
            TestCube.Rotation.Y += 1.0f * Elapsed;

            Vector3[] DrawPoints = new Vector3[8];

            // Rotate, Translate, & Center
            for (int I = 0; I < 8; I++)
            {
                DrawPoints[I] = Center(Translate(Rotate(TestCube.Points[I], TestCube.Rotation), TestCube.Position));
            }

            for (int I = 0; I < 8; I++)
            {
                Vector3 P1 = DrawPoints[I];
                for (int I2 = 0; I2 < 8; I2++)
                {
                    Vector3 P2 = DrawPoints[I2];
                    Canvas.DrawLine((int)P1.X, (int)P1.Y, (int)P2.X, (int)P2.Y, Color.White);
                }
            }
        }

        public Vector3 Center(Vector3 Original)
        {
            return new(
                Original.X + Canvas.Width / 2,
                Original.Y + Canvas.Height / 2,
                Original.Z);
        }
        public Vector3 ApplyPerspective(Vector3 Original)
        {
            return new(
                Original.X * Z0 / (Z0 + Original.Z),
                Original.Y * Z0 / (Z0 + Original.Z),
                Original.Z);
        }
        public static Vector3 Rotate(Vector3 Original, Vector3 Rotation)
        {
            return new(
                Original.X * (float)((Math.Cos(Rotation.Z) * Math.Cos(Rotation.Y)) + Original.Y * (Math.Cos(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Sin(Rotation.X) - Math.Sin(Rotation.Z) * Math.Cos(Rotation.X)) + Original.Z * (Math.Cos(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Cos(Rotation.X) + Math.Sin(Rotation.Z) * Math.Sin(Rotation.Z))),
                Original.X * (float)((Math.Sin(Rotation.Z) * Math.Cos(Rotation.Y)) + Original.Y * (Math.Sin(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Sin(Rotation.X) + Math.Cos(Rotation.Z) * Math.Cos(Rotation.X)) + Original.Z * (Math.Sin(Rotation.Z) * Math.Sin(Rotation.Y) * Math.Cos(Rotation.X) - Math.Cos(Rotation.Z) * Math.Sin(Rotation.X))),
                Original.X * (float)((-Math.Sin(Rotation.Y)) + Original.Y * (Math.Cos(Rotation.Y) * Math.Sin(Rotation.X)) + Original.Z * (Math.Cos(Rotation.Y) * Math.Cos(Rotation.X))),
                Original.U, Original.V, Original.W);
        }
        public static Vector3 Translate(Vector3 Original, Vector3 Translated)
        {
            return new(
                Original.X + Translated.X,
                Original.Y + Translated.Y,
                Original.Z + Translated.Z);
        }
    }
}