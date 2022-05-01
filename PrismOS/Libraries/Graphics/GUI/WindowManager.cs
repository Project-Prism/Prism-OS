using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI
{
    public class WindowManager
    {
        public List<Window> Windows = new();
        public class Window
        {
            public int X, Y, Width, Height, Radius;
            public List<Elements.Element> Elements = new();
            public bool Visible = true, Draggable = true, TitleVisible = true;
            public bool Moving;
            public int IX, IY;
        }
        private bool Dragging = false;

        public void Update(Canvas Canvas)
        {
            for (int I = 0; I < Windows.Count; I++)
            {
                Window Window = Windows[I];

                #region Dragging

                if (Window.Draggable)
                {
                    if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        if (Mouse.X > Window.X && Mouse.X < Window.X + Window.Width && Mouse.Y > Window.Y - 15 && Mouse.Y < Window.Y && !Window.Moving && !Dragging)
                        {
                            Dragging = true;
                            Windows.Remove(Window);
                            Windows.Insert(Windows.Count, Window);
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
                        Canvas.DrawFilledRectangle(Window.X, Window.Y - 15, Window.Width, 15, 0, Color.Black);
                    }
                    Canvas.DrawFilledRectangle(Window.X, Window.Y, Window.Width, Window.Height, Window.Radius, Color.StackOverflowBlack);
                }

                #endregion

                #region Draw Elements

                for (int J = 0; J < Window.Elements.Count; J++)
                {
                    Elements.Element E = Window.Elements[J];

                    #region Calculations

                    if (Windows[0] == Window)
                    {
                        if (E.OnUpdate != null)
                        {
                            E.OnUpdate.Invoke(ref E);
                        }
                        if (E.Clicked && Mouse.MouseState != Cosmos.System.MouseState.Left)
                        {
                            E.Clicked = false;
                            if (E.OnClick != null)
                            {
                                E.OnClick.Invoke(ref E);
                            }
                        }
                        E.Hovering = IsMouseWithin(Window.X + E.X, Window.X + E.Y, E.Width, E.Height);
                        E.Clicked = E.Hovering && Mouse.MouseState == Cosmos.System.MouseState.Left;
                    }

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