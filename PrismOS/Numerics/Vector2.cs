namespace PrismOS.Numerics
{
    public struct Vector2
    {
        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Vector2(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public float X { get; set; }
        public float Y { get; set; }
    }
}
