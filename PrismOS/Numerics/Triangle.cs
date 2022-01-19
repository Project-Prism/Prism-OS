namespace PrismOS.Numerics
{
    public struct Triangle
    {
        public Triangle(Vector3 P1, Vector3 P2, Vector3 P3)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.P3 = P3;
        }

        public Vector3 P1 { get; set; }
        public Vector3 P2 { get; set; }
        public Vector3 P3 { get; set; }
    }
}
