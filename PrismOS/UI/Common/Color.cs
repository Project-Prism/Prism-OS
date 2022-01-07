namespace PrismOS.UI.Common
{
    public struct Color
    {
        public Color(int ARGB)
        {
            this.ARGB = ARGB;
        }
        public Color(int Red, int Green, int Blue)
        {
            ARGB =
                255 << ARGBAlphaShift |
                Red << ARGBRedShift |
                Green << ARGBGreenShift |
                Blue << ARGBBlueShift;
        }
        public Color(int Alpha, int Red, int Green, int Blue)
        {
            ARGB =
                Alpha << ARGBAlphaShift |
                Red << ARGBRedShift |
                Green << ARGBGreenShift |
                Blue << ARGBBlueShift;
        }
        public Color(System.Drawing.Color Color)
        {
            ARGB = Color.ToArgb();
        }

        #region Values
        public int ARGB;
        public int Red => unchecked(ARGB >> ARGBRedShift);
        public int Green => unchecked(ARGB >> ARGBGreenShift);
        public int Blue => unchecked(ARGB >> ARGBBlueShift);
        public int Alpha => unchecked(ARGB >> ARGBAlphaShift);

        private const int ARGBAlphaShift = 24;
        private const int ARGBRedShift = 16;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 0;
        #endregion Values

        public static class KnownColors
        {
            public static readonly Color White = new(-1);
            public static readonly Color Black = new(0);
        }
    }
}
