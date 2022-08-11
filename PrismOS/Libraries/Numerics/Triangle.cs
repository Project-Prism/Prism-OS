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
            Color = Color.White;
        }
        public Triangle(Vector3 P1, Vector3 P2, Vector3 P3, Color Color)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.P3 = P3;
            this.Color = Color;
        }
        public Triangle(Vector3 P1, Vector3 P2, Vector3 P3)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.P3 = P3;
            Color = Color.White;
        }

        public double NormalZ => (P2.X - P1.X) * (P3.Y - P1.Y) - (P2.Y - P1.Y) * (P3.X - P1.X);
        public double AverageZ => (P1.Z + P2.Z + P3.Z) / 3.0;
        public Vector3 P1, P2, P3;
        public Color Color;
    }
}