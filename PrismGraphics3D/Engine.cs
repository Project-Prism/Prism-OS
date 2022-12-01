using PrismGraphics3D.Types;
using PrismGraphics;
using PrismNumerics;

namespace PrismGraphics3D
{
    public class Engine : Graphics
    {
        // To-Do: Implement Camera Rotation
        public Engine(uint Width, uint Height, double FOV) : base(Width, Height)
        {
            this.Height = Height;
            this.Width = Width;
            this.FOV = FOV;

            SkyColor = Color.GoogleBlue;
            Objects = new();
            Camera = new();
            Gravity = 1.0;
        }

        #region Methods

        public void Render()
        {
            double Z0 = Width / 2 / Math.Tan((FOV + Zoom) / 2 * 0.0174532925); // 0.0174532925 == pi / 180

            Clear(SkyColor);

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
        }

        #endregion

        #region Fields

        public List<Mesh> Objects;
        public double Gravity;
        public Color SkyColor;
        public Camera Camera;
        public double Zoom;
        public double FOV;

        #endregion
    }
}