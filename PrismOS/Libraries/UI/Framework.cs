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
            public class Component
            {
                public int X, Y, Width, Height, Radius;
                public string Text;
                public Bitmap Icon;
                public Component Parent;
                public List<Component> Children;
                public delegate void OnClick();
                public delegate void OnCreate();

                public void Draw()
                {
                    foreach (Component aComp in Children)
                    {
                        aComp.Draw();
                    }
                }
            }

            public class Panel : Component
            {
                public new int X, Y, Width, Height;
                public new Component Parent;
                public new List<Component> Children;
                public new delegate void OnClick();
                public new delegate void OnCreate();

                public Panel(int aX, int aY, int aWidth, int aHeight, Component aParent)
                {
                    X = aX;
                    Y = aY;
                    Width = aWidth;
                    Height = aHeight;
                    Parent = aParent;
                }

                public new void Draw()
                {
                    Canvas.DrawFilledRectangle(new Pen(Colorizer.Window.Main), X - (Width / 2), Y - (Width / 2), Width, Height);

                    foreach (Component Child in Children)
                    {
                        Child.Draw();
                    }
                }
            }

            public class Button : Component
            {
                public new int X, Y, Radius;
                public new Component Parent;
                public new List<Component> Children;
                public new delegate void OnClick();
                public new delegate void OnCreate();

                public Button(int aX, int aY, int R, Component aParent)
                {
                    X = aX;
                    Y = aY;
                    Radius = R;
                    Parent = aParent;
                }

                public new void Draw()
                {
                    Canvas.DrawFilledCircle(new Pen(Colorizer.Button.Background), Parent.X + X, Parent.Y + Y, Radius);
                    Canvas.DrawCircle(new Pen(Colorizer.Button.Foreground), Parent.X + X, Parent.Y + Y, Radius + 2);
                    
                    foreach (Component Comp in Children)
                    {
                        Comp.Draw();
                    }

                }
            }

            public class Image : Component
            {
                public new int X, Y, Radius;
                public new Bitmap Icon;
                public new Component Parent;
                public new delegate void OnClick();
                public new delegate void OnCreate();

                public Image(int aX, int aY, Bitmap aImage, Component aParent)
                {
                    X = aX;
                    Y = aY;
                    Icon = aImage;
                    Parent = aParent;
                }

                public new void Draw()
                {
                    Canvas.DrawImageAlpha(Icon, X - ((int)Icon.Width / 2), Y - ((int)Icon.Height / 2));
                }
            }
        }
    }
}
