using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Numerics;
using System;

namespace PrismOS.Libraries.Testing
{
    public class Canvas3D
    {
        public Canvas3D(Canvas Canvas)
        {
            this.Canvas = Canvas;
            MeshCube.Triangles = new Triangle<float>[]
            {
                // South
                new Triangle<float>(0.0f, 0.0f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 1.0f, 0.0f),
                new Triangle<float>(0.0f, 0.0f, 0.0f,   1.0f, 1.0f, 0.0f,   1.0f, 0.0f, 1.0f),

                // East
                new Triangle<float>(1.0f, 0.0f, 0.0f,   1.0f, 1.0f, 0.0f,   1.0f, 1.0f, 1.0f),
                new Triangle<float>(1.0f, 0.0f, 0.0f,   1.0f, 1.0f, 1.0f,   1.0f, 0.0f, 1.0f),
                
                // North
                new Triangle<float>(1.0f, 0.0f, 1.0f,   1.0f, 1.0f, 1.0f,   0.0f, 1.0f, 0.0f),
                new Triangle<float>(1.0f, 0.0f, 1.0f,   0.0f, 1.0f, 1.0f,   0.0f, 0.0f, 0.0f),
                
                // West
                new Triangle<float>(0.0f, 0.0f, 1.0f,   0.0f, 1.0f, 1.0f,   0.0f, 1.0f, 0.0f),
                new Triangle<float>(0.0f, 0.0f, 1.0f,   0.0f, 1.0f, 0.0f,   0.0f, 0.0f, 0.0f),
                
                // Top
                new Triangle<float>(0.0f, 1.0f, 0.0f,   0.0f, 1.0f, 1.0f,   1.0f, 1.0f, 1.0f),
                new Triangle<float>(0.0f, 1.0f, 0.0f,   1.0f, 1.0f, 1.0f,   1.0f, 1.0f, 0.0f),
                
                // Bottom
                new Triangle<float>(1.0f, 0.0f, 1.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f, 0.0f),
                new Triangle<float>(1.0f, 0.0f, 1.0f,   0.0f, 0.0f, 0.0f,   1.0f, 0.0f, 0.0f),
            };
        }

        public float ZFar = 1000.0f, ZNear = 0.1f, FOV = 90.0f;
        public Mesh MeshCube;
        public Canvas Canvas;
        public DateTime LFT;

        public void Update()
        {
            float ElapsedTime = (float)(DateTime.Now - LFT).TotalSeconds;

            #region Projection Matrix

            Matrix<float> ProjectionMatrix = new(4, 4);
            float FOVRad = 1.0f / (float)System.Math.Tan(FOV * 0.5f / 180.0f * 3.14159f); // Suposed to be using System.MathF, but cosmos doesn't support it yet.

            ProjectionMatrix.M[0][0] = Canvas.AspectRatio * FOVRad;
            ProjectionMatrix.M[1][1] = FOVRad;
            ProjectionMatrix.M[2][2] = ZFar / (ZFar - ZNear);
            ProjectionMatrix.M[3][2] = -ZFar * ZNear / (ZFar - ZNear);
            ProjectionMatrix.M[2][3] = 1.0f;
            ProjectionMatrix.M[3][3] = 0.0f;

            #endregion

            #region Draw Triangles

            for (int I = 0; I < MeshCube.Triangles.Length; I++)
            {
                #region Project Triangle

                Triangle<float> TriangleProjected = new();
                MMV(MeshCube.Triangles[I].P1, ref TriangleProjected.P1, ProjectionMatrix);
                MMV(MeshCube.Triangles[I].P2, ref TriangleProjected.P2, ProjectionMatrix);
                MMV(MeshCube.Triangles[I].P3, ref TriangleProjected.P3, ProjectionMatrix);

                #endregion

                #region Scale

                TriangleProjected.P1.X += 1.0f; TriangleProjected.P1.Y += 1.0f;
                TriangleProjected.P2.X += 1.0f; TriangleProjected.P2.Y += 1.0f;
                TriangleProjected.P3.X += 1.0f; TriangleProjected.P3.Y += 1.0f;

                TriangleProjected.P1.X *= 0.5f * Canvas.Width;
                TriangleProjected.P1.Y *= 0.5f * Canvas.Height;
                TriangleProjected.P2.X *= 0.5f * Canvas.Width;
                TriangleProjected.P2.Y *= 0.5f * Canvas.Height;
                TriangleProjected.P3.X *= 0.5f * Canvas.Width;
                TriangleProjected.P3.Y *= 0.5f * Canvas.Height;

                #endregion

                #region Draw Triangle

                Canvas.DrawTriangle(
                    (int)TriangleProjected.P1.X, (int)TriangleProjected.P1.Y,
                    (int)TriangleProjected.P2.X, (int)TriangleProjected.P2.Y,
                    (int)TriangleProjected.P3.X, (int)TriangleProjected.P3.Y, Color.White);

                #endregion
            }

            #endregion

            LFT = DateTime.Now;
        }

        public static void MMV(Vector3<float> In, ref Vector3<float> Out, Matrix<float> Matrix)
        {
            Out.X = In.X * Matrix.M[0][0] + In.Y * Matrix.M[1][0] + In.Z * Matrix.M[2][0] + Matrix.M[3][0];
            Out.X = In.X * Matrix.M[0][1] + In.Y * Matrix.M[1][1] + In.Z * Matrix.M[2][1] + Matrix.M[3][1];
            Out.X = In.X * Matrix.M[0][2] + In.Y * Matrix.M[1][2] + In.Z * Matrix.M[2][2] + Matrix.M[3][2];
            float W = In.X * Matrix.M[0][3] + In.Y * Matrix.M[1][3] + In.Z * Matrix.M[2][3] + Matrix.M[3][3];

            if (W != 0.0f)
            {
                Out.X /= W;
                Out.Y /= W;
                Out.Z /= W;
            }
        }
    }
}