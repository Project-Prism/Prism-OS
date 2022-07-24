namespace PrismOS.Libraries.Numerics
{
    public class Vector3
    {
        public Vector3(double X, double Y, double Z,double U,double V,double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.U = U;
            this.V = V;
            this.W = W;
        }
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3()
        {
        }

        public double X, Y, Z, U, V, W;
    }
}