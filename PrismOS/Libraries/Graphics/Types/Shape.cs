using PrismOS.Libraries.Numerics;
using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.Types
{
    public class Shape
    {
        public List<Triangle> Triangles = new();
        public Vector3 Position = new(0, 0, 0, 0, 0, 0);
        public Vector3 Rotation = new(0, 0, 0, 0, 0, 0);

        public class Cube : Shape
        {
            public Cube(double Width, double Height, double Length)
            {
                Position = new(0, 0, 400);

                // South
                Triangles.Add(new(-(Width / 2), -(Height / 2), -(Length / 2), -(Width / 2), Height / 2, -(Length / 2), Width / 2, Height / 2, -(Length / 2), Color.White));
                Triangles.Add(new(-(Width / 2), -(Height / 2), -(Length / 2), Width / 2, Height / 2, -(Length / 2), Width / 2, -(Height / 2), -(Length / 2), Color.White));

                // East
                Triangles.Add(new(Width / 2, -(Height / 2), -(Length / 2), Width / 2, Height / 2, -(Length / 2), Width / 2, Height / 2, Length / 2, Color.CoolGreen));
                Triangles.Add(new(Width / 2, -(Height / 2), -(Length / 2), Width / 2, Height / 2, Length / 2, Width / 2, -(Height / 2), Length / 2, Color.CoolGreen));

                // North
                Triangles.Add(new(Width / 2, -(Height / 2), Length / 2, Width / 2, Height / 2, Length / 2, -(Width / 2), Height / 2, Length / 2, Color.White));
                Triangles.Add(new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), Height / 2, Length / 2, -(Width / 2), -(Height / 2), Length / 2, Color.White));

                // West
                Triangles.Add(new(-(Width / 2), -(Height / 2), Length / 2, -(Width / 2), Height / 2, Length / 2, -(Width / 2), Height / 2, -(Length / 2), Color.CoolGreen));
                Triangles.Add(new(-(Width / 2), -(Height / 2), Length / 2, -(Width / 2), Height / 2, -(Length / 2), -(Width / 2), -(Height / 2), -(Length / 2), Color.CoolGreen));

                // Top
                Triangles.Add(new(-(Width / 2), Height / 2, -(Length / 2), -(Width / 2), Height / 2, Length / 2, Width / 2, Height / 2, Length / 2, Color.White));
                Triangles.Add(new(-(Width / 2), Height / 2, -(Length / 2), Width / 2, Height / 2, Length / 2, Width / 2, Height / 2, -(Length / 2), Color.White));

                // Bottom
                Triangles.Add(new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), -(Length / 2), Color.White));
                Triangles.Add(new(Width / 2, -(Height / 2), Length / 2, -(Width / 2), -(Height / 2), -(Length / 2), Width / 2, -(Height / 2), -(Length / 2), Color.White));
            }

            public void TestLogic(double ElapsedTime)
            {
                Rotation.Y += ElapsedTime;
            }
        }
    }
}