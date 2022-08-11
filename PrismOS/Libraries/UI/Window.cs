using System.Collections.Generic;
using PrismOS.Libraries.Graphics;
using Cosmos.System;
using System;

namespace PrismOS.Libraries.UI
{
    public class Window : Control
    {
        public Window()
        {
            HasBorder = true;
        }

        // Window Manager Variables
        public static List<Window> Windows { get; set; } = new();
        public static bool Dragging { get; set; } = false;

        // Class Variables
        public List<Control> Elements { get; set; } = new();
        public bool Draggable { get; set; } = true;
        public bool Moving { get; set; } = false;
        public string Text { get; set; } = "";
        public int IX { get; set; }
        public int IY { get; set; }

        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            if (Draggable)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + 20 && !Moving && !Dragging)
                    {
                        Dragging = true;
                        Windows.Remove(this);
                        Windows.Insert(Windows.Count, this);
                        Moving = true;
                        IX = (int)MouseManager.X - X;
                        IY = (int)MouseManager.Y - Y;
                    }
                }
                else
                {
                    Dragging = false;
                    Moving = false;
                }
                if (Moving)
                {
                    X = (int)MouseManager.X - IX;
                    Y = (int)MouseManager.Y - IY;
                }
            }

            if (!IsHidden)
            {
                this.Buffer.DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.Background);

                // Draw Title Bar
                if (HasBorder)
                {
                    this.Buffer.DrawFilledRectangle(0, 0, (int)Width, 20, (int)Theme.Radius, Theme.Accent);
                    this.Buffer.DrawString((int)(Width / 2), 10, Text, Font.Default, Theme.Text, true);
                }

                foreach (Control C in Elements)
                {
                    if (!C.IsHidden)
                    {
                        foreach (Action A in C.OnDrawEvents)
                        {
                            A();
                        }

                        if (MouseManager.X > X + C.X && MouseManager.X < X + C.X + C.Width && MouseManager.Y > Y + C.Y && MouseManager.Y < Y + C.Y + C.Height && (this == Windows[^1] || !Draggable))
                        {
                            C.IsHovering = true;

                            if (MouseManager.MouseState != MouseState.None)
                            {
                                C.IsPressed = true;
                            }
                            else if (C.IsPressed)
                            {
                                C.IsPressed = false;
                                C.OnClick(X - (int)MouseManager.X, Y - (int)MouseManager.Y, MouseManager.LastMouseState);
                            }
                        }
                        else
                        {
                            C.IsHovering = false;
                        }

                        C.OnDraw(this.Buffer);
                    }
                }

                // Draw Surrounding Rectangle
                if (HasBorder)
                {
                    this.Buffer.DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Foreground);
                }

                Buffer.DrawImage(X, Y, this.Buffer, Theme.Radius != 0);
            }
        }

        public override void OnKeyPress(KeyEvent Key)
        {
            foreach (Control C in Elements)
            {
                C.OnKeyPress(Key);
            }
        }
    }
}