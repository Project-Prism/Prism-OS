namespace PrismOS.Numerics
{
    public struct Triangle<T>
    {
        public Triangle(T X1, T Y1, T Z1, T X2, T Y2, T Z2, T X3, T Y3, T Z3)
        {
            P1 = new(X1, Y1, Z1);
            P2 = new(X2, Y2, Z2);
            P3 = new(X3, Y3, Z3);
        }

        public Vector3<T> P1, P2, P3;
    }
}