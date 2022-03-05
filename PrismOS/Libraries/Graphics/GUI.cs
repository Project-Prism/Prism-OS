using Cosmos.System;
using System.Collections.Generic;
using System.Drawing;
using Mouse = Cosmos.System.MouseManager;
using System;

namespace PrismOS.Libraries.Graphics
{
    public static class GUI
    {
        public static List<Window> Windows = new()
        {
            { new(100, 100, 300, 300, 4, "Hewwo, Worwd! UwU") }
        };
        public static int clickX = -100, clickY = -100;
        public static bool clickDown = false;
        public static void Clock(Canvas Canvas)
        {
            foreach (var window in Windows)
            {
                if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y)
                {
                    if (Math.Abs(clickX - Mouse.X) > 4 || Math.Abs(clickY - Mouse.Y) > 4)
                    {
                        window.Dragging((int)Mouse.X, (int)Mouse.Y);
                    }
                }

                window.Render(Canvas);
            }

            if (Mouse.MouseState == MouseState.Left)
            {
                if (!clickDown)
                {
                    clickX = (int)Mouse.X;
                    clickY = (int)Mouse.Y;
                    clickDown = true;
                }
            }
            else if (clickDown)
            {
                clickDown = false;
                if (Math.Abs(clickX - Mouse.X) < 4 && Math.Abs(clickY - Mouse.Y) < 4)
                {
                    Graphics.GUI.ActiveElement = null;
                    foreach (var window in Windows)
                    {
                        if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                        {
                            window.Click(clickX - window.X, clickY - window.Y, 1);
                        }
                    }
                }
                clickX = -100;
                clickY = -100;
            }

            if (ActiveElement != null && KeyboardManager.TryReadKey(out var Key))
            {
                ActiveElement.Key(Key);
            }
        }

        public static Element ActiveElement { get; set; }
        public delegate void ClickDelegate(Element self);

        public abstract class Element
        {
            public int X, Y, Width, Height, Radius;
            public List<Element> Children = new();
            public ClickDelegate OnClick;
            public Element Parent;
            public string Text;

            public abstract void Render(Canvas Canvas);

            public virtual bool Click(int X, int Y, int btn)
            {
                ActiveElement = this;
                return true;
            }

            public virtual bool MouseDown(int X, int Y, int btn)
            {
                return true;
            }

            public virtual bool MouseUp(int X, int Y, int btn)
            {
                return true;
            }

            public virtual bool Key(KeyEvent key)
            {
                return false;
            }

            public void AddChild(Element Element)
            {
                Element.Parent = this;
                Children.Add(Element);
            }
        }

        public class Window : Element
        {
            public Window(int X, int Y, int Width, int Height, int Radius, string Text)
            {
                this.X = X;
                this.Y = Y;
                this.Width = Width;
                this.Height = Height;
                this.Radius = Radius;
                this.Text = Text;
                LastX = X;
                LastY = Y;
            }

            public int LastX, LastY;
            public override bool Click(int X, int Y, int btn)
            {
                ActiveElement = this;
                foreach (var child in Children)
                {
                    int x1 = child.X;
                    int x2 = x1 + child.Width;
                    int y1 = child.Y;
                    int y2 = y1 + child.Height;
                    if (X >= x1 && X <= x2 && Y >= y1 && Y <= y2)
                    {
                        child.Click(X - x1, Y - y1, btn);
                    }
                }
                return true;
            }

            internal void Dragging(int dx, int dy)
            {
                LastX = X;
                LastY = Y;
                X += dx * 2;
                Y += dy * 2;
            }

            public override void Render(Canvas Canvas)
            {
                Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.White);
                Canvas.DrawFilledRectangle(X, Y, Width, 15, Radius, Color.FromArgb(25, 25, 25));
                Canvas.DrawString(X + ((Width / 2) - (Canvas.Font.Default.Width * Text.Length / 2)), Y, Text, Color.White);

                foreach (var child in Children)
                {
                    child.Render(Canvas);
                }
            }
        }

        public class TextBox : Element
        {
            public TextBox(int X, int Y, string Text, bool Typable = false)
            {
                this.X = X;
                this.Y = Y;
                this.Text = Text;
                this.Typable = Typable;
                TextColour = Color.Black;
                Background = Color.Transparent;
            }

            public Color Background;
            public Color TextColour;
            public bool Typable;

            public override void Render(Canvas Canvas)
            {
                Canvas.DrawString(X + Parent.X, Y + Parent.X, Text, TextColour);
            }

            public override bool Key(KeyEvent keyInfo)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKeyEx.Backspace:
                        if (Text.Length > 0)
                        {
                            Text = Text[0..^1];
                        }
                        break;

                    default:
                        if (!KeyboardManager.ControlPressed && keyInfo.KeyChar > (char)0)
                        {
                            Text += keyInfo.KeyChar;
                        }
                        break;
                }
                return true;
            }
        }

        public class Button : Element
        {
            public Button(int X, int Y, int Width, int Height, string text, Color Textclr, Color Backcolor, ClickDelegate onClick)
            {
                this.X = X;
                this.Y = Y;
                this.Width = Width;
                this.Height = Height;
                this.Text = text;
                Background = Backcolor;
                TextColour = Textclr;
                OnClick = onClick;
            }

            public Color Background;
            public Color TextColour;

            public override void Render(Canvas Canvas)
            {
                int mx = X + 4;
                int my = Y + (Height / 2) - 8;
                Canvas.DrawFilledRectangle(X + Parent.X - 1, Y + Parent.Y - 1, Width + 2, Height + 2, 2, Color.White);
                Canvas.DrawFilledRectangle(X + Parent.X, Y + Parent.Y, Width, Height, 2, Color.Gray);
                Canvas.DrawString(mx + Parent.X, my + Parent.Y, Text, Color.White);
            }

            public override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

        /*

        internal class Image : Element
        {
            public Image(int X, int Y, Bitmap Bitmap, Element Parent = null)
            {
                this.X = X;
                this.Y = Y;
                img = Bitmap;
                this.Parent = Parent;
            }

            public new Element Parent;
            public Bitmap img;

            internal override void Render(Canvas Canvas, int offset_x, int offset_y)
            {
                Canvas.DrawBitmap(Parent.X + X, Parent.Y + Y, img);
            }
        }

        internal class Switch : Element
        {
            public Switch(int X, int Y, Color Fore, Color Back, ClickDelegate OnClick, Element Parent = null)
            {
                this.X = X;
                this.Y = Y;
                Width = 0;
                Height = 0;
                this.Parent = Parent;
                this.OnClick = OnClick;
                status = false;
                this.Fore = Fore;
                this.Back = Back;
            }

            public Color Back;
            public Color Fore;
            public bool status;
            public ClickDelegate OnClick;

            internal override void Render(Canvas Canvas, int offset_x, int offset_y)
            {
                Canvas.DrawFilledRectangle(offset_x + X - 1, offset_y + Y - 1, 39, 23, 9, Fore);
                Canvas.DrawFilledRectangle(offset_x + X, offset_y + Y, 37, 21, 9, Back);
                if (status) { Canvas.DrawFilledCircle(offset_x + X + 9, offset_y + Y + 10, 9, Color.White); }
                if (!status) { Canvas.DrawFilledCircle(offset_x + X + 16, offset_y + Y + 10, 9, Color.White); }
            }

            internal override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

        internal class Div : Element
        {
            public Div(int X, int Y, int Width, int Height, Color Color, Element Parent = null)
            {
                this.X = X;
                this.Y = Y;
                this.Width = Width;
                this.Height = Height;
                this.Parent = Parent;
                this.Color = Color;
            }

            public Color Color;

            internal override void Render(Canvas Canvas, int offset_x, int offset_y)
            {
                Canvas.DrawLine(offset_x + X, offset_y + Y, Width, offset_y + Y, Color);
                Canvas.DrawLine(offset_x + X, offset_y + Y + Height, Width, offset_y + Y + Height, Color);
            }
        }

        /*
        internal class Toggle : Element
        {
            public Color Fore;
            public Color Back;
            public bool enable;
            public delegate void ClickDelegate(Toggle self);
            public ClickDelegate OnClick;

            public Toggle(int x, int y, Color Foreground, Color Background, bool state, ClickDelegate onClick) : base(x, y)
            {
                OnClick = onClick;
                Fore = Foreground;
                Back = Background;
                enable = state;
            }

            internal override void Render(G_lib draw, int offset_x, int offset_y)
            {
                draw.Rounded_Box(Fore, offset_x + X - 1, offset_y + Y - 1, Driver.screenX / 80 + 2, Driver.screenY / 45 + 2, 2);
                draw.Rounded_Box(Back, offset_x + X, offset_y + Y, Driver.screenX / 80, Driver.screenY / 45, 2);
                if (enable) { draw.Rounded_Box(Fore, offset_x + X + 2, offset_y + Y + 2, Driver.screenX / 80 - 4, Driver.screenY / 45 - 4, 2); }
            }

            internal override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }
        */
    }
}