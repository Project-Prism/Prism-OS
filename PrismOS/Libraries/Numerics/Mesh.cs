namespace PrismOS.Libraries.Numerics
{
    public struct Mesh
    {
        public Mesh(Triangle[] Triangles)
        {
            this.Triangles = Triangles;
        }

        public Triangle[] Triangles;
    }
}