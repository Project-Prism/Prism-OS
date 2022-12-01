namespace PrismNumerics
{
	public class Matrix
	{
		#region Methods

		/// <summary>
		/// Gets the projection matrix for 3D rendering.
		/// </summary>
		/// <param name="Width">Width of the canvas.</param>
		/// <param name="Height">Height of the canvas.</param>
		/// <param name="FOV">Field of view.</param>
		/// <param name="Far">Far plane, can be ignored.</param>
		/// <param name="Near">Near plane, can be ignored.</param>
		/// <returns>3D Projection matrix.</returns>
		public static Matrix GetProjectionMatrix(uint Width, uint Height, double FOV = 90.0, double Far = 1000.0, double Near = 0.1)
		{
			double FOVRadius = 1.0 / Math.Tan(FOV * 0.5 / 180.0 * 3.14159);

			return new Matrix()
			{
				M11 = Height / Width * FOVRadius, // Aspect ratio * FOV
				M22 = FOVRadius,
				M33 = Far / (Far - Near),
				M43 = -Far * Near / (Far - Near),
				M34 = 1.0,
				M44 = 0.0,
			};
		}
		
		/// <summary>
		/// Multiplies a vector with a matrix.
		/// </summary>
		/// <param name="Input">Input matrix to multiply.</param>
		/// <param name="Matrix">Matrix to multiply by.</param>
		/// <returns>Input multiplied by Matrix.</returns>
		public static Vector3 Multiply(Vector3 Input, Matrix Matrix)
		{
			Vector3 Output = new()
			{
				X = Input.X * Matrix.M11 + Input.Y * Matrix.M21 + Input.Z * Matrix.M31 + Matrix.M41,
				Y = Input.X * Matrix.M12 + Input.Y * Matrix.M22 + Input.Z * Matrix.M32 + Matrix.M42,
				Z = Input.X * Matrix.M13 + Input.Y * Matrix.M23 + Input.Z * Matrix.M33 + Matrix.M43,
				W = Input.X * Matrix.M14 + Input.Y * Matrix.M24 + Input.Z * Matrix.M34 + Matrix.M44,
			};

			if (Output.W != 0.0)
			{
				Output.X /= Output.W;
				Output.Y /= Output.W;
				Output.Z /= Output.W;
			}

			return Output;
		}

		#endregion

		#region Fields

		public double M11, M12, M13, M14;
		public double M21, M22, M23, M24;
		public double M31, M32, M33, M34;
		public double M41, M42, M43, M44;

		#endregion
	}
}