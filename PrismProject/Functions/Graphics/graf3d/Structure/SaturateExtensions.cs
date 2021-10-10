namespace graf3d.Engine.Structure
{
    /// <summary>
    ///     Rozszerzenie przycinające wartości zmiennoprzecinkowe do zakresu 0-1.
    /// </summary>
    public static class SaturateExtensions
    {
        public static float Saturate(this float x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x > 1 ? 1 : x;
        }

        public static double Saturate(this double x)
        {
            if (x < 0)
            {
                return 0;
            }
            return x > 1 ? 1 : x;
        }

        public static Color Saturate(this Color vector)
        {
            return new Color(
                Saturate(vector.R),
                Saturate(vector.G),
                Saturate(vector.B),
                Saturate(vector.A)
                );
        }
    }
}