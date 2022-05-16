using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class WindowManager : List<Elements.Window>
    {
        private bool Dragging = false;
        public int GlobalRadius = 6;

        public void Update(Canvas Canvas)
        {
            for (int I = 0; I < Count; I++)
            {
                Elements.Window Window = this[I];

                #region Dragging

                if (Window.Draggable)
                {
                    if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        if (Mouse.X > Window.X && Mouse.X < Window.X + Window.Width && Mouse.Y > Window.Y - 15 && Mouse.Y < Window.Y && !Window.Moving && !Dragging)
                        {
                            Dragging = true;
                            Remove(Window);
                            Insert(Count, Window);
                            Window.Moving = true;
                            Window.IX = (int)Mouse.X - Window.X;
                            Window.IY = (int)Mouse.Y - Window.Y;
                        }
                    }
                    else
                    {
                        Dragging = false;
                        Window.Moving = false;
                    }
                    if (Window.Moving)
                    {
                        Window.X = (int)Mouse.X - Window.IX;
                        Window.Y = (int)Mouse.Y - Window.IY;
                    }
                }

                #endregion

                #region Draw Window

                if (Window.Visible)
                {
                    if (Window.TitleVisible)
                    {
                        Canvas.DrawFilledRectangle(Window.X, Window.Y - 15, Window.Width, 15, 0, Color.StackOverflowOrange);
                        Canvas.DrawString(Window.X, Window.Y - 15, Window.Text, Color.White);
                    }
                    Canvas.DrawFilledRectangle(Window.X, Window.Y, Window.Width, Window.Height, Window.Radius, Color.StackOverflowBlack);
                    Canvas.DrawRectangle(Window.X - 1, Window.Y - 1, Window.Width + 1, Window.Height + 1, Window.Radius, Color.White);
                }

                #endregion

                #region Draw Elements

                for (int J = 0; J < Window.Elements.Count; J++)
                {
                    Elements.Element E = Window.Elements[J];

                    #region Calculations

                    if (E.OnUpdate != null)
                    {
                        E.OnUpdate.Invoke(ref E, ref Window);
                    }
                    if (E.Clicked && Mouse.MouseState != Cosmos.System.MouseState.Left && !Dragging)
                    {
                        E.Clicked = false;
                        if (E.OnClick != null)
                        {
                            E.OnClick.Invoke(ref E, ref Window);
                        }
                    }
                    E.Hovering = IsMouseWithin(Window.X + E.X, Window.Y + E.Y, E.Width, E.Height);
                    E.Clicked = E.Hovering && Mouse.MouseState == Cosmos.System.MouseState.Left;

                    #endregion

                    E.Update(Canvas, Window);
                }

                #endregion
            }
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height;
        }
    }
}