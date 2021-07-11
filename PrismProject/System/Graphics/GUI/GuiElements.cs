﻿using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrismProject
{
    abstract class BaseGuiElement
    {
        public int X, Y, Width, Height;

        public BaseGuiElement Parent;

        public BaseGuiElement(int x, int y, int w, int h, BaseGuiElement parent = null)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Parent = parent;
        }

        internal abstract void Render(drawable draw, int offset_x, int offset_y);
        internal virtual bool Click(int x, int y, int btn)
        {
            Desktop.ActiveElement = this;
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

    class GuiWindow : BaseGuiElement
    {
        public string Title = "";
        public List<BaseGuiElement> Children;

        private int lastX, lastY;
        private int titleHeight;

        public GuiWindow(string title, int x, int y, int w, int h) : base(x, y, w, h)
        {
            Title = title;
            Children = new List<BaseGuiElement>();
            lastX = x;
            lastY = y;
            titleHeight = Driver.screenY / 25;
        }

        public void AddChild(BaseGuiElement guiElement)
        {
            guiElement.Parent = this;
            Children.Add(guiElement);
        }

        internal override bool Click(int x, int y, int btn)
        {
            Desktop.ActiveElement = this;
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
            lastX = X;
            lastY = Y;
            X += dx * 2;
            Y += dy * 2;
        }

        internal override void Render(drawable draw, int offset_x = 0, int offset_y = 0)
        {
            if (lastX != X || lastY != Y) // we moved
            {
                draw.Rounded_Box(Desktop.Background, lastX, lastY, Width, Height);
                lastX = X;
                lastY = Y;
            }
            draw.Window(Driver.font, X, Y, Width, Height, Title, true);

            foreach (var child in Children)
            {
                child.Render(draw, X, Y + titleHeight);
            }
        }
    }

    class GuiText : BaseGuiElement
    {
        public string Value;
        public Color TextColour;

        public GuiText(string text, int x, int y) : base(x, y, 0, 0)
        {
            Value = text;
            TextColour = Desktop.Text;
        }

        internal override void Render(drawable draw, int offset_x, int offset_y)
        {
            draw.Text(TextColour, Driver.font, Value, X + offset_x, Y + offset_y);
        }
    }

    class GuiTextBox : BaseGuiElement
    {
        public string Value;
        public Color Background;
        public Color TextColour;

        public GuiTextBox(int x, int y, int w = 150) : base(x, y, w, 15)
        {
            Background = Color.White;
            TextColour = Desktop.Text;
        }

        internal override bool Click(int x, int y, int btn)
        {
            // todo: Add support for moving cursor for text entry
            return base.Click(x, y, btn);
        }

        internal override bool Key(KeyEvent keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKeyEx.Backspace:
                    if (Value.Length > 0)
                    {
                        Value = Value.Substring(0, Value.Length - 1);
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

        internal override void Render(drawable draw, int offset_x, int offset_y)
        {
            string txt = Value + (Desktop.ActiveElement == this ? "|" : "");
            draw.Textbox(Driver.font, txt, Background, TextColour, X + offset_x, Y + offset_y, Width);
        }
    }

    class GuiButton : BaseGuiElement
    {
        public string Value;
        public Color Background;
        public Color TextColour;

        public delegate void ClickDelegate(GuiButton self);
        public ClickDelegate OnClick;

        public GuiButton(string text, ClickDelegate onClick, int x, int y, int w, int h = 16) : base(x, y, w, h)
        {
            Value = text;
            Background = Desktop.Button;
            TextColour = Desktop.Text;
            OnClick = onClick;
        }

        internal override void Render(drawable draw, int offset_x, int offset_y)
        {
            //int mx = X + (Width / 2) - (Value.Length * 8);
            int mx = X + 4;
            int my = Y + (Height / 2) - 8;
            draw.Rounded_Box(Background, X + offset_x, Y + offset_y, Width, Height, 4);
            draw.Text(TextColour, Driver.font, Value, mx + offset_x, my + offset_y);
        }

        internal override bool Click(int x, int y, int btn)
        {
            OnClick(this);
            return base.Click(x, y, btn);
        }
    }
}