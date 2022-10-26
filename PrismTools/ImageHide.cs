using PrismGL2D;

namespace PrismTools
{
	/// <summary>
	/// ImageHide class
	/// Hides an image's contents from sight unless not drawn with alpha or converted back.
	/// </summary>
	public static class ImageHide
	{
		public static Graphics UnHide(Graphics G)
		{
			Graphics Temp = new(G.Width, G.Height);

			for (int I = 0; I < G.Size; I++)
			{
				Temp[I] = Color.FromARGB(255, G[I].R, G[I].G, G[I].B);
			}

			return Temp;
		}
		public static Graphics Hide(Graphics G)
		{
			Graphics Temp = new(G.Width, G.Height);

			for (int I = 0; I < G.Size; I++)
			{
				Temp[I] = Color.FromARGB(0, G[I].R, G[I].G, G[I].B);
			}

			return Temp;
		}
	}
}