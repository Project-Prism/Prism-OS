namespace PrismOS.Libraries.Numerics
{
    public class Vector2<T>
    {
        public Vector2(T X, T Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public T X, Y;
    }
}