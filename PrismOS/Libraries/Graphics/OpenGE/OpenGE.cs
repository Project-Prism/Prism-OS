using PrismOS.Libraries.Numerics;
using System;

namespace PrismOS.Libraries.Graphics.OpenGE
{
    public class OpenGE
    {
        public Mesh TestCube = Shapes.Cube;

        public void Update(Canvas Canvas, float ElapsedTime)
        {
            for (int I = 0; I < TestCube.Triangles.Length; I++)
            {
                Triangle<float> Triangle = ProjectTriangle(TestCube.Triangles[I], Canvas.Width, Canvas.Height, 1000.0f, 0.1f, 90.0f);
                Canvas.DrawTriangle(Triangle, Color.White);
            }
        }

        public static Triangle<float> ProjectTriangle(Triangle<float> Triangle, int Width, int Height, float ZFar, float ZNear, float FOV)
        {
            float A = Width / Height;
            float F = 1 / (float)Math.Tan(FOV * 0.5f / 180.0f * 3.14159f);
            float Q = ZFar / (ZFar - ZNear);
            float AF = A * F;

            return new()
            {
                P1 = new(AF * Triangle.P1.X / Triangle.P1.Z, F * Triangle.P1.Y / Triangle.P1.Z, (Triangle.P1.Z * Q) - (ZNear * Q)),
                P2 = new(AF * Triangle.P2.X / Triangle.P2.Z, F * Triangle.P2.Y / Triangle.P2.Z, (Triangle.P2.Z * Q) - (ZNear * Q)),
                P3 = new(AF * Triangle.P3.X / Triangle.P3.Z, F * Triangle.P3.Y / Triangle.P3.Z, (Triangle.P3.Z * Q) - (ZNear * Q)),
            };
        }
    }
}