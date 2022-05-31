using Mouse = Cosmos.System.MouseManager;
using PrismOS.Libraries.Graphics.GUI;
using PrismOS.Libraries.Graphics;
using System.Collections.Generic;

namespace PrismOS.Libraries
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
                
                Window.Update(Canvas.Current);

                foreach (Element E in Window.Elements)
                {
                    #region Calculations

                    E.OnUpdate.Invoke();

                    if (IsMouseWithin(Window.X + E.X, Window.Y + E.Y, E.Width, E.Height) && Window == Windows[^1])
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
            Canvas.Current.DrawImage(Canvas.Current.Width / 2 - 128, Canvas.Current.Height / 2 - 128, 256, 256, Files.Resources.Logo);
            Canvas.Current.DrawString(Canvas.Current.Width / 2, Canvas.Current.Height / 2 + 128, "Shutting down...", Color.White, true);
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