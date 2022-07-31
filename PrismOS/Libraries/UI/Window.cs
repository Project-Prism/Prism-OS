using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;
using PrismOS.Libraries.Graphics;
using System;

namespace PrismOS.Libraries.UI
{
    public class Window : Control
    {
        // Window Manager Variables
        public static List<Window> Windows { get; set; } = new();
        public static bool Dragging { get; set; } = false;

        // Class Variables
        public List<Control> Elements { get; set; } = new();
        public bool TitleVisible { get; set; } = true;
        public bool Draggable { get; set; } = true;
        public bool Moving { get; set; } = false;
        public string Text { get; set; } = "";
        public Theme Theme { get; set; }
        public int IX { get; set; }
        public int IY { get; set; }

        public void Update(FrameBuffer Canvas)
        {
            if (Draggable)
            {
                if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                {
                    if (Mouse.X > X && Mouse.X < X + Width && Mouse.Y > Y && Mouse.Y < Y + 20 && !Moving && !Dragging)
                    {
                        Dragging = true;
                        Windows.Remove(this);
                        Windows.Insert(Windows.Count, this);
                        Moving = true;
                        IX = (int)Mouse.X - X;
                        IY = (int)Mouse.Y - Y;
                    }
                }
                else
                {
                    Dragging = false;
                    Moving = false;
                }
                if (Moving)
                {
                    X = (int)Mouse.X - IX;
                    Y = (int)Mouse.Y - IY;
                }
            }

            if (IsVisible)
            {
                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Theme.Radius, Theme.Background);

                if (TitleVisible)
                {
                    FrameBuffer.DrawFilledRectangle(0, 0, Width, 20, Theme.Radius, Theme.Accent);
                    FrameBuffer.DrawString(Width / 2, 10, Text, Theme.Font, Theme.Foreground, true);
                }

                foreach (Control C in Elements)
                {
                    C.OnUpdate.Invoke();

                    if (Mouse.X > X + C.X && Mouse.X <  X + C.X + C.Width && Mouse.Y > Y + C.Y && Mouse.Y < Y + C.Y + C.Height && (this == Windows[^1] || !Draggable))
                    {
                        C.IsHovering = true;
                        if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                        {
                            C.IsPressed = true;
                        }
                        else
                        {
                            if (C.IsPressed)
                            {
                                C.IsPressed = false;
                                C.OnClick();
                            }
                        }
                    }
                    else
                    {
                        C.IsHovering = false;
                    }

                    C.Update(this);
                }

                if (HasBorder)
                {
                    FrameBuffer.DrawRectangle(0, 0, Width - 1, Height - 1, Theme.Radius, Theme.Foreground);
                }

                Canvas.DrawImage(X, Y, FrameBuffer, false);
            }
        }
        public override void Update(Window Parent)
        {
            throw new NotImplementedException();
        }

        public override void OnKey(Cosmos.System.KeyEvent Key)
        {
            foreach (Control C in Elements)
            {
                C.OnKey(Key);
            }
        }
    }
}