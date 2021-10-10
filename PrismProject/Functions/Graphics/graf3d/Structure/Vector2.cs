using System;

namespace graf3d.Engine.Structure
{
    public struct Vector2
    {
        public readonly float X;
        public readonly float Y;

        /// <summary>
        ///     Tworzy wektor o danych komponentach.
        /// </summary>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Oblicza długość wektora.
        /// </summary>
        public float Length()
        {
            return (float) Math.Sqrt(X*X + Y*Y);
        }

        /// <summary>
        ///     Dodaje dwa wektory.
        /// </summary>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        ///     Odejmuje dwa wektory.
        /// </summary>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        ///     Mnoży wektor przez skalar.
        /// </summary>
        public static Vector2 operator *(Vector2 left, float value)
        {
            return new Vector2(left.X*value, left.Y*value);
        }
    }
}