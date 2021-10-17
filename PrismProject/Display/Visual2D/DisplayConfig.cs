using Cosmos.System.Graphics;
using System.Drawing;

namespace PrismProject.Display.Visual2D
{
    class DisplayConfig
    {
        public enum AnchorPoint
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }

        public static int Width = 800;
        public static int Height = 600;
        public static Canvas Controler = FullScreenCanvas.GetFullScreenCanvas(new Mode(Width, Height, ColorDepth.ColorDepth32));
        public static string SelectedMode = "FullScreenDisplay @" + Controler.Mode.Rows + "x" + Controler.Mode.Columns + " (" + Controler.Mode.ColorDepth + ")";


        public static Color[] Button_theme = new Color[] { Color.White, Color.FromArgb(25, 25, 30), Color.White };
        public static Color[] Windows_theme = new Color[] { Color.White, Color.White, Color.Gray, Color.White };
        public static Color[] MsgBox_theme = new Color[] { Color.FromArgb(25, 25, 30), Color.White, Color.White };
    }
}
