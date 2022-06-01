using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class Window
    {
        public bool Visible = true, Draggable = true, TitleVisible = true, Moving;
        public Canvas.Font Font = Canvas.Font.Default;
        public int X, Y, Width, Height, Radius, IX, IY;
        public List<Element> Elements = new();
        public Theme Theme;
        public string Text;

        public void Update(Canvas Canvas)
        {
            if (Visible)
            {
                if (TitleVisible)
                {
                    Canvas.DrawFilledRectangle(X, Y - 15, Width, 15, 0, Theme.Accent);
                    Canvas.DrawString(X, Y - 15, Text, Font, Theme.Foreground);
                }
                Canvas.DrawRectangle(X, Y, Width - 1, Height - 1, Radius, Theme.Foreground);
            }
        }
    }
}