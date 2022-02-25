namespace PrismOS.Numerics
{
    public class Vector3
    {
        public Vector3()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }
        public Vector3(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3(double X, double Y, double Z)
        {
            this.X = (float)X;
            this.Y = (float)Y;
            this.Z = (float)Z;
        }

        public float X;
        public float Y;
        public float Z;
    }
}