using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics
{
    public class Gradient : List<List<Color>>
    {
        public Gradient(Color Color1, Color Color2, int Width, int Height)
        {
            for (int i = 0; i < Width; i++)
            {
                Add(new());
                for (int j = 0; j < Height; j++)
                {
                    this[i].Add(new Color(Color1.A, (byte)(Color1.R + (Color2.R - Color1.R) * i / Width), (byte)(Color1.G + (Color2.G - Color1.G) * i / Width), (byte)(Color1.B + (Color2.B - Color1.B) * i / Width)));
                }
            }
        }
    }
}
