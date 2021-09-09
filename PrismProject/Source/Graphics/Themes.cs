using Cosmos.System.Graphics;
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
        public static Pen[] Window = { new Pen(Color.FromArgb(25, 25, 25)), new Pen(Color.White), new Pen(Color.AntiqueWhite), new Pen(Color.Black), new Pen(Color.Black) };
        /// <summary>
            /// [0] = White
            /// [1] = Black
            /// [2] = Red
            /// [3] = Green
            /// [4] = Blue
            /// [5] = Grey
            /// </summary>
        public static Pen[] Common = { new Pen(Color.White), new Pen(Color.Black), new Pen(Color.Red), new Pen(Color.Green), new Pen(Color.Blue), new Pen(Color.DarkSlateGray) };
    }
}
