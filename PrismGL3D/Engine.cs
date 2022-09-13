using PrismGL3D.Numerics;
using PrismGL3D.Objects;
using PrismGL3D.Types;
using PrismGL2D.UI;
using PrismGL2D;

namespace PrismGL3D
{
    public class Engine : Control
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
        public Camera Camera;
        public int FOV;

        #endregion

        public void Render(Graphics Graphics)
        {
            if (Width != Graphics.Width || Height != Graphics.Height)
            {
                Width = Graphics.Width;
                Height = Graphics.Height;
            }

            double Z0 = Width / 2 / Math.Tan(FOV / 2 * 0.0174532925); // 0.0174532925 == pi / 180

            Clear();

            // Calculate Objects
            for (int O = 0; O < Objects.Count; O++)
            {
                #region Physics

                if (Objects[O].HasPhysics)
                {
                    Objects[O].Step(Gravity, 1.0);
                }

                #endregion

                Triangle[] DrawTriangles = Objects[O].Triangles.ToArray();

                for (int T = 0; T < Objects[O].Triangles.Count; T++)
                {
                    DrawTriangles[T] = DrawTriangles[T].Rotate(Objects[O].Rotation);
                    DrawTriangles[T] = DrawTriangles[T].Translate(Objects[O].Position + Camera.Position);
                    DrawTriangles[T] = DrawTriangles[T].ApplyPerspective(Z0);
                    DrawTriangles[T] = DrawTriangles[T].Center(Width, Height);

                    if (DrawTriangles[T].GetNormal() < 0)
                    {
                        DrawFilledTriangle(
                            (int)DrawTriangles[T].P1.X, (int)DrawTriangles[T].P1.Y,
                            (int)DrawTriangles[T].P2.X, (int)DrawTriangles[T].P2.Y,
                            (int)DrawTriangles[T].P3.X, (int)DrawTriangles[T].P3.Y,
                            DrawTriangles[T].Color);
                    }
                }
            }

            // Draw Buffer
            Graphics.DrawImage(X, Y, this, false);
        }
    }
}