using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Button : Element
    {
        public string Text;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible)
            {
                Color Background = Clicked ? Color.Black : Hovering ? Color.LightGray : Color.White;
                Color Foreground = Clicked ? Color.White : Hovering ? Color.Black : Color.Black;

                int SX = Parent.X + X + (Text.Length * Canvas.Font.Default.Width);
                int SY = Parent.Y + Y + (Text.Split('\n').Length * Canvas.Font.Default.Height);

                Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Background);
                Canvas.DrawString(SX, SY, Text, Foreground);
            }
        }
    }
}