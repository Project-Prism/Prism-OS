using System;
using System.Collections.Generic;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS.Libraries.Graphics
{
    public class ContentPage
    {
        public ContentPage(int X, int Y, int Width, int Height, int Radius, string Text, Bitmap Icon, List<Element> Children = default)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Radius = Radius;
            this.Text = Text;
            this.Icon = Icon;
            if (Children != default)
            {
                this.Children = Children;
            }
        }

        public bool Moving;
        public string Text;
        public Bitmap Icon;
        public delegate void OnClick(ref Element This);
        public List<Element> Children;
        public int X, Y, Width, Height, Radius;
        public int IX, IY;

        public class Element
        {
            public Element(int X, int Y, int Width, int Height, int Radius, string Text, Bitmap Icon, ContentPage Parent, byte Type, OnClick ClickEvent = null)
            {
                this.X = X;
                this.Y = Y;
                this.Width = Width;
                this.Height = Height;
                this.Radius = Radius;
                this.Text = Text;
                this.Icon = Icon;
                this.Parent = Parent;
                this.Type = Type;
                this.ClickEvent = ClickEvent;
            }

            public byte Type;
            public string Text;
            public Bitmap Icon;
            public ContentPage Parent;
            public OnClick ClickEvent;
            public bool Clicked, Hovering;
            public int X, Y, Width, Height, Radius;
        }

        public void Update(Canvas Canvas)
        {
            if (Mouse.MouseState == Cosmos.System.MouseState.Left && Mouse.X > X && Mouse.X < X + Width && Mouse.Y > Y - 15 && Mouse.Y < Y && !Moving)
            {
                Moving = true;
                IX = (int)Mouse.X - X;
                IY = (int)Mouse.Y - Y;
            }
            else
            {
                Moving = false;
            }
            if (Moving)
            {
                X = (int)Math.Clamp(Mouse.X - IX, 0, Canvas.Width - Width);
                Y = (int)Math.Clamp(Mouse.Y - IY, 15, Canvas.Height - (Height + 15));
            }

            Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.SystemColors.BackGround);
            Canvas.DrawFilledRectangle(X, Y - 15, Width, 15, Radius, Color.SystemColors.ForeGround);
            Canvas.DrawString(X, Y - 15, Text, Color.SystemColors.TitleText);

            for (int I = 0; I < Children.Count; I++)
            {
                Element E = Children[I];
                E.Hovering = IsMouseWithin(E.X, E.Y, E.Width, E.Height);
                E.Clicked = E.Hovering && Mouse.MouseState == Cosmos.System.MouseState.Left;
                if (E.Clicked && Mouse.MouseState != Cosmos.System.MouseState.Left)
                {
                    E.Clicked = false;
                    if (E.ClickEvent != null)
                    {
                        E.ClickEvent.Invoke(ref E);
                    }
                }

                if (E.Type == 0x00)
                {
                    return;
                } // Empty
                if (E.Type == 0x01)
                {
                    Canvas.DrawString(
                        Math.Clamp(X + E.X, 0, Canvas.Width - (Canvas.Font.Default.Width * Text.Length)),
                        Math.Clamp(Y + E.Y, 0, Canvas.Height - (Canvas.Font.Default.Height * Text.Split('\n').Length)),
                        E.Text, Color.SystemColors.ContentText);
                } // Label
                if (E.Type == 0x02)
                {
                    Color C;
                    if (E.Hovering && E.Clicked)
                    {
                        C = Color.SystemColors.ButtonClick;
                    }
                    else if (E.Hovering)
                    {
                        C = Color.SystemColors.ButtonHighlight;
                    }
                    else
                    {
                        C = Color.SystemColors.Button;
                    }
                    Canvas.DrawFilledRectangle(E.X, E.Y, E.Width, E.Height, E.Radius, C);
                    Canvas.DrawString(E.X, E.Y, E.Text, Color.SystemColors.TitleText);
                } // Button
            }
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height;
        }
    }
}