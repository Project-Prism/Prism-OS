using System;

namespace graf3d.Engine.Structure
{
    /// <summary>
    ///     Struktura implementująca kwaternion. Będzie używana do wykonywania obrotów w przestrzeni trójwymiarowej.
    /// </summary>
    public struct Quaternion
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Kąt yaw (w radianach).
        /// </summary>
        public double Yaw => Math.Atan2(2.0*(Y*Z + W*X), W*W - X*X - Y*Y + Z*Z);

        /// <summary>
        ///     Kąt pitch (w radianach).
        /// </summary>
        public double Pitch => Math.Asin(-2.0*(X*Z - W*Y));

        /// <summary>
        ///     Kąt roll (w radianach).
        /// </summary>
        public double Roll => Math.Atan2(2.0*(X*Y + W*Z), W*W + X*X - Y*Y - Z*Z);

        /// <summary>
        ///     Tworzy kwaternion obrotu o kąty yaw, pitch i roll.
        /// </summary>
        public static Quaternion RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            var halfRoll = roll*0.5;
            var halfPitch = pitch*0.5;
            var halfYaw = yaw*0.5;

            var sinRoll = Math.Sin(halfRoll);
            var cosRoll = Math.Cos(halfRoll);
            var sinPitch = Math.Sin(halfPitch);
            var cosPitch = Math.Cos(halfPitch);
            var sinYaw = Math.Sin(halfYaw);
            var cosYaw = Math.Cos(halfYaw);

            return new Quaternion(
                (float) (cosYaw*sinPitch*cosRoll + sinYaw*cosPitch*sinRoll),
                (float) (sinYaw*cosPitch*cosRoll - cosYaw*sinPitch*sinRoll),
                (float) (cosYaw*cosPitch*sinRoll - sinYaw*sinPitch*cosRoll),
                (float) (cosYaw*cosPitch*cosRoll + sinYaw*sinPitch*sinRoll)
                );
        }

        /// <summary>
        ///     Tworzy kwaternion obrotu wokół dowolnej osi o dowolny kąt.
        /// </summary>
        /// <param name="axis">
        ///     Oś obrotu.
        /// </param>
        /// <param name="angle">
        ///     Kąt w radianach.
        /// </param>
        public static Quaternion RotationAxis(Vector3 axis, float angle)
        {
            var normalized = axis.Normalize();

            var halfAngle = angle*0.5f;
            var sin = (float) Math.Sin(halfAngle);

            return new Quaternion(
                normalized.X*sin,
                normalized.Y*sin,
                normalized.Z*sin,
                (float) Math.Cos(halfAngle));
        }

        /// <summary>
        ///     Mnoży dwa kwaterniony. Uwaga: aby łączyć rotacje należy mnożyć kwaterniony od prawej do lewej.
        /// </summary>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            var lx = left.X;
            var ly = left.Y;
            var lz = left.Z;
            var lw = left.W;
            var rx = right.X;
            var ry = right.Y;
            var rz = right.Z;
            var rw = right.W;
            var a = ly*rz - lz*ry;
            var b = lz*rx - lx*rz;
            var c = lx*ry - ly*rx;
            var d = lx*rx + ly*ry + lz*rz;

            return new Quaternion(
                lx*rw + rx*lw + a,
                ly*rw + ry*lw + b,
                lz*rw + rz*lw + c,
                lw*rw - d);
        }
    }
}