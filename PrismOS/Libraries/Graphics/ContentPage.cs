using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;
using PrismOS.Libraries.Formats;
using System;

namespace PrismOS.Libraries.Graphics
{
    public class ContentPage
    {
        public ContentPage()
        {
        }

        #region Values
        public bool Moving;
        public string Text;
        public Bitmap Icon;
        public List<Element> Children;
        public int X, Y, IX, IY, Width, Height, Radius;
        public delegate void Event(ref Element This);
        #endregion

        public class Element
        {
            public Element()
            {
            }

            public byte Type;
            public string Text;
            public Bitmap Icon;
            public ContentPage Parent;
            public Event OnClick, OnUpdate;
            public bool Clicked, Hovering;
            public int X, Y, Width, Height, Radius;
        }

        public void Update(Canvas Canvas)
        {
            #region Dragging
            if (Mouse.MouseState==Cosmos.System.MouseState.Left)
            {
                if (Mouse.X > X && Mouse.X < X + Width && Mouse.Y > Y - 15 && Mouse.Y < Y && !Moving)
                {
                    Moving=true;
                    IX=(int)Mouse.X-X;
                    IY=(int)Mouse.Y-Y;
                }
            }
            else
            {
                Moving=false;
            }
            if (Moving)
            {
                X = (int)Mouse.X - IX;
                Y = (int)Mouse.Y - IY;
            }
            #endregion

            #region Drawing
            Canvas.DrawFilledRectangle(X, Y, Width, Height, Radius, Color.SystemColors.BackGround);
            Canvas.DrawFilledRectangle(X, Y - 15, Width, 15, Radius, Color.SystemColors.ForeGround);
            Canvas.DrawString(X, Y - 15, Text, Color.SystemColors.TitleText);
            #endregion

            for (int I = 0; I < Children.Count; I++)
            {
                Element E = Children[I];
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
                E.Hovering = IsMouseWithin(X + E.X, X + E.Y, E.Width, E.Height);
                E.Clicked = E.Hovering && Mouse.MouseState == Cosmos.System.MouseState.Left;

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
                    Canvas.DrawFilledRectangle(X + E.X, Y + E.Y, E.Width, E.Height, E.Radius, C);

                    Canvas.DrawString(
                        Math.Clamp(X + E.X, 0, Canvas.Width - (Canvas.Font.Default.Width * Text.Length)),
                        Math.Clamp(Y + E.Y, 0, Canvas.Height - (Canvas.Font.Default.Height * Text.Split('\n').Length)),
                        E.Text, Color.SystemColors.TitleText);
                } // Button
                if (E.Type == 0x03)
                {
                    if (E.Icon != null)
                    {
                        if (Width != 0 && Height != 0)
                        {
                            Canvas.DrawBitmap(E.X, E.Y, E.Width, E.Height, E.Icon);
                        }
                        else
                        {
                            Canvas.DrawBitmap(E.X, E.Y, E.Icon);
                        }
                    }
                } // Image
            }
        }

        public static bool IsMouseWithin(int X, int Y, int Width, int Height)
        {
            return Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height;
        }
    }
}