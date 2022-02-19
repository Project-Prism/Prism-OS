using System.Numerics;

namespace PrismOS.Common
{
    public struct Triangle
    {
        public Triangle(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3)
        {
            V1 = new(X1, Y1, Z1);
            V2 = new(X2, Y2, Z2);
            V3 = new(X3, Y3, Z3);
        }

        public Vector3 V1;
        public Vector3 V2;
        public Vector3 V3;
    }
}