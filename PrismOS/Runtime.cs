using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;
using PrismOS.Graphics.GUI;
using PrismOS.Graphics;

namespace PrismOS
{
    public static class Runtime
    {
        public static void Update()
        {
            foreach (Application App in Applications)
            {
                App.OnUpdate();
            }
            foreach (Window Window in Windows)
            {
                if (Window.Draggable)
                {
                    if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                    {
                        if (Mouse.X > Window.Position.X && Mouse.X < Window.Position.X + Window.Size.Width && Mouse.Y > Window.Position.Y - 15 && Mouse.Y < Window.Position.Y && !Window.Moving && !Dragging)
                        {
                            Dragging = true;
                            Windows.Remove(Window);
                            Windows.Insert(Windows.Count, Window);
                            Window.Moving = true;
                            Window.IX = (int)Mouse.X - Window.Position.X;
                            Window.IY = (int)Mouse.Y - Window.Position.Y;
                        }
                    }
                    else
                    {
                        Dragging = false;
                        Window.Moving = false;
                    }
                    if (Window.Moving)
                    {
                        Window.Position.X = (int)Mouse.X - Window.IX;
                        Window.Position.Y = (int)Mouse.Y - Window.IY;
                    }
                }
                
                Window.Update(Canvas.Current);

                foreach (Element E in Window.Elements)
                {
                    #region Calculations

                    E.OnUpdate.Invoke();

                    if (IsMouseWithin(Window.Position.X + E.Position.X, Window.Position.Y + E.Position.Y, E.Size.Width, E.Size.Height) && Window == Windows[^1])
                    {
                        E.Hovering = true;
                        if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                        {
                            E.Clicked = true;
                        }
                        else
                        {
                            if (E.Clicked)
                            {
                                E.Clicked = false;
                                E.OnClick();
                            }
                        }
                    }
                    else
                    {
                        E.Hovering = false;
                    }

                    #endregion

                    if (Window.Visible && E.Visible)
                    {
                        E.Update(Canvas.Current, Window);
                    }
                }
            }
        }

        public static List<Application> Applications = new();
        public static List<Window> Windows = new();
        public static bool Dragging = false;

        public abstract class Application
        {
            public Application()
            {
                OnCreate();
                Applications.Add(this);
            }
            ~Application()
            {
                OnDestroy();
                Applications.Remove(this);
            }

            public Stack<byte> Stack = new();

            public abstract void OnUpdate();
            public abstract void OnDestroy();
            public abstract void OnCreate();
        }

        public static void Stop()
        {
            Canvas.Current.Clear();
            Canvas.Current.DrawImage((int)(Canvas.Current.Width / 2 - 128), (int)(Canvas.Current.Height / 2 - 128), 256, 256, Assets.Logo);
            Canvas.Current.DrawString((int)(Canvas.Current.Width / 2), (int)(Canvas.Current.Height / 2 + 128), "Shutting down...", Canvas.Font.Default, Color.White, true);
            Canvas.Current.Update(false);
            foreach (Application App in Applications)
            {
                App.OnDestroy();
            }
            Cosmos.System.Power.Shutdown();
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.X <= X + Width && Mouse.Y >= Y && Mouse.Y <= Y + Height;
        }
    }
}