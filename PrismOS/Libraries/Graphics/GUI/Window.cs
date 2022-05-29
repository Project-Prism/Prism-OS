using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class Window
    {
        public int X, Y, Width, Height, Radius;
        public List<Element> Elements = new();
        public bool Visible = true, Draggable = true, TitleVisible = true, Moving;
        public Theme Theme;
        public string Text;
        public int IX, IY;

        public void Update(Canvas Canvas)
        {
            if (Visible)
            {
                if (TitleVisible)
                {
                    Canvas.DrawFilledRectangle(X, Y - 15, Width, 15, 0, Theme.Accent);
                    Canvas.DrawString(X, Y - 15, Text, Theme.Foreground);
                }
                foreach (Element E in Elements)
                {
                    #region Calculations

                    if (E.OnUpdate != null)
                    {
                        E.OnUpdate.Invoke();
                    }
                    if (E.Clicked && Mouse.MouseState != Cosmos.System.MouseState.Left && !Runtime.Dragging)
                    {
                        E.Clicked = false;
                        if (E.OnClick != null)
                        {
                            E.OnClick.Invoke();
                        }
                    }
                    E.Hovering = Mouse.X >= E.X && Mouse.X <= E.X + E.Width && Mouse.Y >= E.Y && Mouse.Y <= E.Y + E.Height;
                    E.Clicked = E.Hovering && Mouse.MouseState == Cosmos.System.MouseState.Left;

                    #endregion
                    
                    E.Update(Canvas, this);
                }
                Canvas.DrawRectangle(X, Y, Width - 1, Height - 1, Radius, Theme.Foreground);
            }
        }
    }
}