using PrismOS.Libraries.Numerics;
using System.Collections.Generic;

namespace PrismOS.Libraries.Rasterizer.Objects
{
    public class Mesh
    {
        public List<Triangle> Triangles = new();
        public Vector3 Position = new(0, 0, 0, 0, 0, 0);
        public Vector3 Rotation = new(0, 0, 0, 0, 0, 0);
        public Vector3 Velocity = new(0, 0, 0, 0, 0, 0);
        public Vector3 Force = new(0, 0, 0, 0, 0, 0);
        public bool HasCollision = false;
        public bool HasPhysics = false;
        public double Mass = 1.0;

        public void Step(double Gravity, double DT)
        {
            Force.Y += Mass * Gravity;
            Velocity.Y += Force.Y / Mass * DT;
            Position.Y += Velocity.Y * DT;
            Force = new(0, 0, 0, 0, 0, 0);
        }
    }
}