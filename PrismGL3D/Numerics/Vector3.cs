namespace PrismGL3D.Numerics
{
    public class Vector3
    {
        public Vector3(double X, double Y, double Z,double U,double V,double W)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.U = U;
            this.V = V;
            this.W = W;
        }
        public Vector3(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public Vector3() { }

        #region Operators

        public static Vector3 operator +(Vector3 V1, Vector3 V2) => new(V1.X + V2.X, V1.Y + V2.Y, V1.Z + V2.Z);
        public static Vector3 operator -(Vector3 V1, Vector3 V2) => new(V1.X - V2.X, V1.Y - V2.Y, V1.Z - V2.Z);
        public static Vector3 operator *(Vector3 V1, Vector3 V2) => new(V1.X * V2.X, V1.Y * V2.Y, V1.Z * V2.Z);
        public static Vector3 operator /(Vector3 V1, Vector3 V2) => new(V1.X / V2.X, V1.Y / V2.Y, V1.Z / V2.Z);

        public static Vector3 operator +(Vector3 V1, double V) => new(V1.X + V, V1.Y + V, V1.Z + V);
        public static Vector3 operator -(Vector3 V1, double V) => new(V1.X - V, V1.Y - V, V1.Z - V);
        public static Vector3 operator *(Vector3 V1, double V) => new(V1.X * V, V1.Y * V, V1.Z * V);
        public static Vector3 operator /(Vector3 V1, double V) => new(V1.X / V, V1.Y / V, V1.Z / V);

        public static Vector3 operator *(Vector3 V1, Matrix M1) => Matrix.Multiply(V1, M1);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the cross product of two vectors.
        /// </summary>
        /// <param name="V1">Vector 1.</param>
        /// <param name="V2">Vector 2.</param>
        /// <returns>Cross product of vector 1 and vector 2.</returns>
        public static Vector3 GetCrossProduct(Vector3 V1, Vector3 V2)
        {
            return new(
                V1.Y * V2.Z - V1.Z * V2.Y,
                V1.Z * V2.X - V1.X * V2.Z,
                V1.X * V2.Y - V1.Y * V2.X);
        }
        /// <summary>
        /// Gets the dot product of the vector.
        /// </summary>
        /// <returns>The dot product of this vector.</returns>
        public double GetDotProduct()
        {
            return X * X + Y * Y + Z * Z;
        }

        #endregion

        #region Fields

        public double X, Y, Z;
        public double U, V, W;

		#endregion
	}
}