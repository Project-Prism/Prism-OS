namespace PrismOS.Libraries.Numerics
{
    public class Vector3<T>
    {
        public Vector3(T X, T Y, T Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public T X, Y, Z;
    }
}