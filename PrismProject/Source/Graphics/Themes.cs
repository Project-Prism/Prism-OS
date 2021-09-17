using System.Drawing;

namespace PrismProject.Source.Graphics
{
    class Themes
    {
        /// <summary>
            /// [0] = Title bar color
            /// [1] = Title text color
            /// [2] = Window color
            /// [3] = Window text color
            /// [4] = shadow color
        /// </summary>
        public static Color[] Window = { Color.FromArgb(25, 25, 25), Color.White, Color.AntiqueWhite, Color.Black, Color.Black };
        /// <summary>
            ///  [0] = Background
            ///  [1] = Foreground
        /// </summary>
        public static Color[] ProgBar = { Color.DimGray, Color.White };
    }
}
