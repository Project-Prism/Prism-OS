namespace PrismOS.Libraries.Numerics
{
    public struct Triangle
    {
        public Triangle(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3)
        {
            P = new Vector3[3];
            P[0] = new(X1, Y1, Z1);
            P[1] = new(X2, Y2, Z2);
            P[2] = new(X3, Y3, Z3);
        }

        public Vector3[] P;
    }
}