namespace PrismOS.Libraries.Numerics
{
    public class Vector3
    {
        public Vector3()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            U = 0.0f;
            V = 0.0f;
            W = 0.0f;
        }
        public Vector3(float X, float Y, float Z, float U = 0.0f, float V = 0.0f, float W = 0.0f)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.U = U;
            this.V = V;
            this.W = W;
        }

        public float X, Y, Z, U, V, W;
    }
}