using System;

namespace graf3d.Engine.Structure
{
    public struct Vector3
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        /// <summary>
        ///     Tworzy wektor o komponentach X, Y, Z.
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 Zero => new Vector3(0, 0, 0);
        public static Vector3 One => new Vector3(1, 1, 1);
        public static Vector3 UnitX => new Vector3(1, 0, 0);
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        public static Vector3 UnitZ => new Vector3(0, 0, 1);

        /// <summary>
        ///     Przekształca wektor do wektora jednostkowego.
        ///     Zwrot i kierunek zostanie zachowany, lecz długość wektora będzie równa 1.
        /// </summary>
        public Vector3 Normalize()
        {
            return this*(1/Length());
        }

        /// <summary>
        ///     Oblicza długość wektora.
        /// </summary>
        public float Length()
        {
            return (float) Math.Sqrt(X*X + Y*Y + Z*Z);
        }

        /// <summary>
        ///     Przekształca wektor położenia za pomocą macierzy.
        /// </summary>
        public static Vector3 TransformCoordinate(Vector3 coord, Matrix transf)
        {
            var x = coord.X*transf.M[0, 0] + coord.Y*transf.M[1, 0] + coord.Z*transf.M[2, 0] + transf.M[3, 0];
            var y = coord.X*transf.M[0, 1] + coord.Y*transf.M[1, 1] + coord.Z*transf.M[2, 1] + transf.M[3, 1];
            var z = coord.X*transf.M[0, 2] + coord.Y*transf.M[1, 2] + coord.Z*transf.M[2, 2] + transf.M[3, 2];
            var w = coord.X*transf.M[0, 3] + coord.Y*transf.M[1, 3] + coord.Z*transf.M[2, 3] + transf.M[3, 3];
            return new Vector3(x/w, y/w, z/w);
        }

        /// <summary>
        ///     Zwraca iloczyn skalarny dwóch wektorów.
        /// </summary>
        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.X*right.X + left.Y*right.Y + left.Z*right.Z;
        }

        /// <summary>
        ///     Zwraca iloczyn wektorowy dwóch wektorów.
        /// </summary>
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            return new Vector3(
                left.Y*right.Z - left.Z*right.Y,
                left.Z*right.X - left.X*right.Z,
                left.X*right.Y - left.Y*right.X);
        }

        /// <summary>
        ///     Dodaje dwa wektory.
        /// </summary>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        ///     Odejmuje dwa wektory.
        /// </summary>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        ///     Mnoży wektor przez skalar.
        /// </summary>
        public static Vector3 operator *(Vector3 left, float value)
        {
            return new Vector3(left.X*value, left.Y*value, left.Z*value);
        }
    }
}