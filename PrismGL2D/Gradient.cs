namespace PrismGL2D
{
	/// <summary>
	/// Gradient class, used for generating gradients.
	/// INCOMPLETE/BROKEN: https://dev.to/ndesmic/linear-color-gradients-from-scratch-1a0e
	/// </summary>
	public class Gradient : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Gradient"/> class.
		/// </summary>
		/// <param name="Width">Width of the gradient.</param>
		/// <param name="Height">Height of the gradient.</param>
		/// <param name="Colors">Colors to use.</param>
		public Gradient(uint Width, uint Height, Color[] Colors) : base(Width, Height)
		{
			int XOffset = 0;

			for (int I = 0; I < Colors.Length - 1; I++)
			{
				Draw(XOffset += (int)(Height / Colors.Length), 0, Width, (uint)(Height / Colors.Length), Colors[I], Colors[I + 1]);
			}
		}

		internal void Draw(int X, int Y, uint Width, uint Height, Color C1, Color C2)
		{
			for (int Y2 = Y; Y2 < Y + Height; Y2++)
			{
				for (int X2 = X; X2 < X + Width; X2++)
				{
					this[X2, Y2] = C1 + ((C2 - C1) * (Y2 - Y) / Height);
				}
			}
		}
	}
}