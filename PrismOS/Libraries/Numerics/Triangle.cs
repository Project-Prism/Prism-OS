using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.Numerics
{
    public struct Triangle
    {
        public Triangle(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3, Color Color)
        {
            P1 = new(X1, Y1, Z1);
            P2 = new(X2, Y2, Z2);
            P3 = new(X3, Y3, Z3);
            this.Color = Color;
        }
        public Triangle(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3)
        {
            P1 = new(X1, Y1, Z1);
            P2 = new(X2, Y2, Z2);
            P3 = new(X3, Y3, Z3);
        }

        public Color Color = Color.White;
        public Vector3 P1, P2, P3;

        public void Draw(FrameBuffer Canvas, uint X = 0, uint Y = 0)
        {
            Canvas.DrawTriangle(
                (int)(X + P1.X), (int)(Y + P1.Y), (int)(X + P2.X),
                (int)(Y + P2.Y), (int)(X + P3.X), (int)(Y + P3.Y), Color);
        }
    }
}