using System.Collections.Generic;
using PrismOS.Libraries.Numerics;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class Window
    {
        public bool Visible = true, Draggable = true, TitleVisible = true, Moving;
        public List<Element> Elements = new();
        public Position Position;
        public Size Size;
        public Theme Theme;
        public string Text;
        public int IX, IY;

        public void Update(Canvas Canvas)
        {
            if (Visible)
            {
                Canvas.DrawFilledRectangle(
                    Position.X,
                    Position.Y - (TitleVisible ? 20 : 0),
                    Size.Width, Size.Height,
                    Theme.Radius,
                    Theme.Background);
                
                if (TitleVisible)
                {
                    Canvas.DrawFilledRectangle(
                        Position.X,
                        Position.Y - 20,
                        Size.Width,
                        20,
                        Theme.Radius,
                        Theme.Accent);
                    
                    Canvas.DrawString(
                        Position.X + Size.Width / 2,
                        Position.Y - 20,
                        Text,
                        Theme.Font,
                        Theme.Foreground,
                        true);
                    
                    Canvas.DrawRectangle(
                        Position.X,
                        Position.Y - 20,
                        Size.Width - 1,
                        Size.Height - 1,
                        Theme.Radius,
                        Theme.Foreground);
                }
            }
        }
    }
}