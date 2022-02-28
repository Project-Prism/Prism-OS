/*
using Cosmos.System;
using System.Collections.Generic;
using System.Drawing;
using Bitmap = Cosmos.System.Graphics.Bitmap;

namespace PrismOS.Graphics
{
    public static class UI
    {
        public static Element ActiveElement { get; set; }
        public delegate void ClickDelegate(Element self);

        public abstract class Element
        {
            public int X, Y, Width, Height;
            public Element Parent;

            internal abstract void Render(Canvas Canvas, int offset_x, int offset_y);

            internal virtual bool Click(int x, int y, int btn)
            {
                ActiveElement = this;
                return true;
            }

            internal virtual bool MouseDown(int x, int y, int btn)
            {
                return true;
            }

            internal virtual bool MouseUp(int x, int y, int btn)
            {
                return true;
            }

            internal virtual bool Key(KeyEvent keyInfo)
            {
                return false;
            }
        }

        public class Window : Element
        {
            public Window(int X, int Y, int Width, int Height, string Title, int Radius) : base(X, Y, Width, Height)
            {
                LastX = X;
                LastY = Y;
                this.Title = Title;
                this.Radius = Radius;
                Children = new List<Element>();
            }

            public string Title;
            public List<Element> Children;
            public int LastX, LastY, Radius;

            public void AddChild(Element guiElement)
            {
                guiElement.Parent = this;
                Children.Add(guiElement);
            }

            internal override bool Click(int x, int y, int btn)
            {
                ActiveElement = this;
                foreach (var child in Children)
                {
                    int x1 = child.X;
                    int x2 = x1 + child.Width;
                    int y1 = child.Y;
                    int y2 = y1 + child.Height;
                    if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                    {
                        child.Click(x - x1, y - y1, btn);
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

            internal override void Render(Canvas Canvas, int offset_x = 0, int offset_y = 0)
            {
                if (LastX != X || LastY != Y) // we moved
                {
                    Canvas.DrawFilledRectangle(LastX, LastY, Width, Height, Radius, Color.White);
                    LastX = X;
                    LastY = Y;
                }
                Canvas.DrawFilledRectangle(X - 1, Y - 1, Width + 2, Height + 2, Radius, Color.DeepSkyBlue);
                Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.White);
                Canvas.DrawFilledRectangle(X, Y, Width, 50, Radius, Color.DeepSkyBlue);
                Canvas.DrawString(X, Y, Title, Color.White);

                foreach (var child in Children)
                {
                    child.Render(Canvas, X, Y + 50);
                }
            }
        }

        internal class TextBox : Element
        {
            public TextBox(int X, int Y, string Text, bool Typable = false) : base(X, Y, 0, 0)
            {
                Value = Text;
                TextColour = Color.Black;
                Background = Color.Transparent;
                this.Typable = Typable;
            }

            public string Value;
            public Color Background;
            public Color TextColour;
            public bool Typable;

            internal override void Render(Canvas Canvas, int offset_x, int offset_y)
            {
                Canvas.DrawString(X + offset_x, Y + offset_y, Value, TextColour);
            }

            internal override bool Key(KeyEvent keyInfo)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKeyEx.Backspace:
                        if (Value.Length > 0)
                        {
                            Value = Value[0..^1];
                        }
                        break;

                    default:
                        if (!KeyboardManager.ControlPressed && keyInfo.KeyChar > (char)0)
                        {
                            Value += keyInfo.KeyChar;
                        }
                        break;
                }
                return true;
            }
        }

        internal class Button : Element
        {
            public string Value;
            public Color Background;
            public Color TextColour;

            public delegate void ClickDelegate(Button self);

            public ClickDelegate OnClick;

            public Button(int x, int y, int w, int h, string text, Color Textclr, Color Backcolor, ClickDelegate onClick)
            {
                Value = text;
                Background = Backcolor;
                TextColour = Textclr;
                OnClick = onClick;
            }

            internal override void Render(Canvas Canvas, int offset_x, int offset_y)
            {
                int mx = X + 4;
                int my = Y + (Height / 2) - 8;
                Canvas.DrawFilledRectangle(X + offset_x - 1, Y + offset_y - 1, Width + 2, Height + 2, 2, Color.White);
                Canvas.DrawFilledRectangle(X + offset_x, Y + offset_y, Width, Height, 2, Color.Gray);
                Canvas.DrawString(mx + offset_x, my + offset_y, Value, Color.White);
            }

            internal override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

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
    }
}
*/