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
            public string Text;
            public bool Moving, Minimized;
            public int IX, IY;
        }
        private bool Dragging = false;

        public void Update(Canvas Canvas)
        {
            for (int I = 0; I < Windows.Count; I++)
            {
                Window Window = Windows[I];
                if (!Window.Minimized)
                {

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
        }

        public void ShowMessage(string Title, string Contents, string Button)
        {
            Window W = new()
            {
                X = (Kernel.Canvas.Width / 2) - 200 + (10 * (Windows.Count - 1)),
                Y = (Kernel.Canvas.Height / 2) - 75 + (10 * (Windows.Count - 1)),
                Width = 400,
                Height = 150,
                Radius = Kernel.GlobalRadius,
                Text = Title,
                Elements = new()
                {
                    new Elements.Button()
                    {
                        X = 400 - 15,
                        Y = -15,
                        Width = 15,
                        Height = 15,
                        Radius = Kernel.GlobalRadius,
                        Text = "X",
                        OnClick = (ref Elements.Element E, ref Window Parent) => { Windows.Remove(Parent); },
                    },
                    new Elements.Label()
                    {
                        X = 200,
                        Y = 75,
                        Center = true,
                        Text = Contents,
                        Color = Color.White,
                    },
                    new Elements.Button()
                    {
                        X = 360,
                        Y = 138,
                        Width = 40,
                        Height = 12,
                        Radius = Kernel.GlobalRadius,
                        Text = Button,
                        OnClick = (ref Elements.Element E, ref Window Parent) => { Windows.Remove(Parent); },
                    },
                },
            };
            Windows.Add(W);
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height;
        }
    }
}