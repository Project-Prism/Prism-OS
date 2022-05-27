using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI
{

    public class Window
    {
        public int X, Y, Width, Height, Radius;
        public List<Element> Elements = new();
        public bool Visible = true, Draggable = true, TitleVisible = true, Moving;
        public string Text;
        public int IX, IY;

        public void Update(Canvas Canvas)
        {
            if (Visible)
            {
                if (TitleVisible)
                {
                    Canvas.DrawFilledRectangle(X, Y - 15, Width, 15, 0, Color.StackOverflowOrange);
                    Canvas.DrawString(X, Y - 15, Text, Color.White);
                }
                Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.StackOverflowBlack);
                Canvas.DrawRectangle(X, Y, Width, Height, Radius, Color.White);
            }
        }
    }
}