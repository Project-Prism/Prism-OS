using System;
using System.Collections.Generic;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS.Libraries.Graphics
{
    public class ContentPage
    {
        public ContentPage(int X, int Y, int Width, int Height, int Radius, string Text, Bitmap Icon)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Radius = Radius;
            this.Text = Text;
            this.Icon = Icon;
            Children = new();
        }

        public bool Moving;
        public string Text;
        public Bitmap Icon;
        public List<Element> Children;
        public int X, Y, Width, Height, Radius;

        public class Element
        {
            public Element(int X, int Y, int Width, int Height, int Radius, string Text, Bitmap Icon, ContentPage Parent, byte Type)
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
            }

            public byte Type;
            public string Text;
            public Bitmap Icon;
            public ContentPage Parent;
            public int X, Y, Width, Height, Radius;
        }

        public void Update(Canvas Canvas)
        {
            int IX = X, IY = Y;
            if (Mouse.MouseState != Cosmos.System.MouseState.Left)
            {
                Moving = false;
            }
            if (IsMouseWithin(X, Y, Width, 15) && Mouse.MouseState == Cosmos.System.MouseState.Left && !Moving)
            {
                Moving = true;
                IX = (int)Mouse.X - X;
                IY = (int)Mouse.Y - Y;
            }
            if (Moving)
            {
                X = (int)Math.Clamp(Mouse.X - IX, 0, Canvas.Width - Width);
                Y = (int)Math.Clamp(Mouse.Y - IY, 15, Canvas.Height - (Height + (15 - (5 * 2))));
            }

            Canvas.DrawFilledRectangle(IX, IY, Width, Height, Radius, Color.SystemColors.BackGround);
            Canvas.DrawFilledRectangle(IX, IY, Width, 15, Radius, Color.SystemColors.ForeGround);
            Canvas.DrawString(IX, IY, Text, Color.SystemColors.TitleText);

            foreach (Element E in Children)
            {
                if (E.Type == 0x00)
                {
                    return;
                } // Empty
                if (E.Type == 0x01)
                {
                    Canvas.DrawString(
                        X + E.X,
                        Y + E.Y,
                        E.Text, Color.SystemColors.ContentText);
                } // Label
            }
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height;
        }
    }
}