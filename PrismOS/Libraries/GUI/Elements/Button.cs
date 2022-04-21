using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.GUI.Elements
{
    public class Button : Element
    {
        public string Text;

        public override void Update(Canvas Canvas)
        {
            if (Visible)
            {
                if (Clicked)
                {
                    Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.Black);
                    Canvas.DrawString(X, Y, Text, Color.White);
                }
                else if (Hovering)
                {
                    Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.LightGray);
                    Canvas.DrawString(X, Y, Text, Color.Black);
                }
                else
                {
                    Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.White);
                    Canvas.DrawString(X, Y, Text, Color.Black);
                }
            }
        }
    }
}