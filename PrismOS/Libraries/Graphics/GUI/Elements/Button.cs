using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Button : Element
    {
        public Color ClickedBackground = Color.Black;
        public Color ClickedForeground = Color.White;
        public Color HoveringBackground = Color.LightGray;
        public Color HoveringForeground = Color.Black;
        public Color Background = Color.White;
        public Color Foreground = Color.Black;
        public string Text;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible)
            {
                Color BG = Clicked ? ClickedBackground : Hovering ? HoveringBackground : Background;
                Color FG = Clicked ? ClickedForeground : Hovering ? HoveringForeground : Foreground;

                int SX = Parent.X + X + ((Width / 2) - Text.Length * Canvas.Font.Default.Width / 2);
                int SY = Parent.Y + Y + ((Height / 2) - (Text.Split('\n').Length * Canvas.Font.Default.Height / 2));

                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, BG);
                Canvas.DrawString(SX, SY, Text, FG);
            }
        }
    }
}