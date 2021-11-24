using Cosmos.System;
using Cosmos.System.Graphics;
using System.Collections.Generic;

namespace Prism.Libraries.UI
{
    /// <summary>
    /// The basics for the GUI
    /// </summary>
    public static class Framework
    {
        public static int Width { get; set; } = 800;
        public static int Height { get; set; } = 600;
        public static int MouseX { get; } = (int)MouseManager.X;
        public static int MouseY { get; } = (int)MouseManager.Y;
        private static Mode Mode { get; set; } = new(Width, Height, ColorDepth.ColorDepth32);
        public static Canvas Canvas { get; } = FullScreenCanvas.GetFullScreenCanvas(Mode);

        public static bool MouseIsWithin(int X1, int Y1, int X2, int Y2)
        {
            return MouseX > X1 && MouseX < X2 && MouseY > Y1 && MouseY < Y2;
        }

        public static bool IsClicked()
        {
            return MouseManager.MouseState == MouseState.Left || MouseManager.MouseState == MouseState.Right;
        }

        public static class UI
        {
            public abstract class Component
            {
                public int X, Y, Width, Height, Radius;
                public string Text;
                public string[] Texts;
                public Bitmap Icon;
                public Bitmap[] Icons;
                public Component Parent;
                public List<Component> Children;

                public void Draw()
                {
                    foreach (Component aComp in Children)
                    {
                        aComp.Draw();
                    }
                }

                public void OnClick()
                {
                }

                public void OnCreate()
                {

                }
            }

            public class Window : Component
            {
                public new int X, Y, Width, Height;
                public new string Text;
                public new Bitmap Icon;
                public new List<Component> Children;

                public Window(int aX, int aY, int aWidth, int aHeight, string aTitle, Bitmap aIcon)
                {
                    X = aX;
                    Y = aY;
                    Width = aWidth;
                    Height = aHeight;
                    Text = aTitle;
                    Icon = aIcon;

                    OnCreate();
                }

                public new void Draw()
                {
                    Canvas.DrawFilledRectangle(new Pen(Colorizer.Window.Main), X - (Width / 2), Y - (Width / 2), Width, Height);

                    foreach (Component Child in Children)
                    {
                        Child.Draw();
                    }
                }

                public new void OnClick()
                {
                }

                public new void OnCreate()
                {
                }
            }

            public class Button : Component
            {
                public new int X, Y, Radius;
                public new Bitmap Icon;
                public new Component Parent;

                public Button(int aX, int aY, int R, Component aParent)
                {
                    X = aX;
                    Y = aY;
                    Radius = R;
                    Parent = aParent;

                    OnCreate();
                }

                public new void Draw()
                {
                    Canvas.DrawFilledCircle(new Pen(Colorizer.Button.Background), Parent.X + X, Parent.Y + Y, Radius);
                    Canvas.DrawCircle(new Pen(Colorizer.Button.Foreground), Parent.X + X, Parent.Y + Y, Radius + 2);
                    Canvas.DrawImageAlpha(Icon, Parent.X + X - ((int)Icon.Width / 2), Parent.Y + Y - ((int)Icon.Height / 2));
                }

                public new void OnClick()
                {
                }

                public new void OnCreate()
                {
                }
            }
        }
    }
}
