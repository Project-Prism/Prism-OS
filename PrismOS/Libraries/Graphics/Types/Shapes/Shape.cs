using PrismOS.Libraries.Numerics;
using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.Types.Shapes
{
    public class Shape
    {
        public List<Triangle> Triangles = new();
        public Vector3 Position = new(0, 0, 0, 0, 0, 0);
        public Vector3 Rotation = new(0, 0, 0, 0, 0, 0);

    }
}