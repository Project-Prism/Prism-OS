namespace PrismGL3D.Numerics
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

        public static Vector3 GetCrossProduct(Vector3 V1, Vector3 V2)
        {
            return new(
                V1.Y * V2.Z - V1.Z * V2.Y,
                V1.Z * V2.X - V1.X * V2.Z,
                V1.X * V2.Y - V1.Y * V2.X);
        }
        public double GetDotProduct()
        {
            return X * X + Y * Y + Z * Z;
        }
        public Vector3 GetNormal()
        {
            return new(X / 1, Y / 1, Z / 1);
        }

        public static Vector3 operator +(Vector3 V1, Vector3 V2) => new(V1.X + V2.X, V1.Y + V2.Y, V1.Z + V2.Z);
        public static Vector3 operator -(Vector3 V1, Vector3 V2) => new(V1.X - V2.X, V1.Y - V2.Y, V1.Z - V2.Z);
        public static Vector3 operator *(Vector3 V1, Vector3 V2) => new(V1.X * V2.X, V1.Y * V2.Y, V1.Z * V2.Z);
        public static Vector3 operator /(Vector3 V1, Vector3 V2) => new(V1.X / V2.X, V1.Y / V2.Y, V1.Z / V2.Z);

        public static Vector3 operator +(Vector3 V1, double V) => new(V1.X + V, V1.Y + V, V1.Z + V);
        public static Vector3 operator -(Vector3 V1, double V) => new(V1.X - V, V1.Y - V, V1.Z - V);
        public static Vector3 operator *(Vector3 V1, double V) => new(V1.X * V, V1.Y * V, V1.Z * V);
        public static Vector3 operator /(Vector3 V1, double V) => new(V1.X / V, V1.Y / V, V1.Z / V);
    }
}