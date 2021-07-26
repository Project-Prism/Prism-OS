using Cosmos.System;
using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;

namespace PrismProject
{
    internal class Themes
    {
        public struct Window
        {
            public static Color Window_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color Window_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when not focused
            public static Color Window_Theme_Inner = Color.FromArgb(60, 60, 65); //inner background color
            public static Color Window_Title_Text = Color.White; //Text color
        }

        public struct Button
        {//button colors
            public static Color Button_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color Button_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color Button_Theme_Inner = Color.FromArgb(40, 40, 45); //inner background color
            public static Color Button_Text = Color.White; //text color
        }

        public struct R_Button
        {//radio button colors
            public static Color R_Button_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color R_Button_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color R_Button_Theme_Inner = Color.FromArgb(40, 40, 45); //inner background color
        }

        public struct Switch
        {//switch colors
            public static Color Switch_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color Switch_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color Switch_Theme_Inner = Color.FromArgb(40, 40, 45); //inner background color
            public static Color Switch_Nub = Color.White; //switch nub color
        }

        public struct Divider
        {//divider colors
            public static Color Div_Theme = Color.FromArgb(0, 202, 78); //Default color
        }

        public struct Textbox
        {//text box colors
            public static Color TB_Border = Color.FromArgb(0, 202, 78); //Default color
            public static Color TB_Border_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color YB_Inner = Color.FromArgb(40, 40, 45); //inner background color
        }

        public static Color Desktop_main = Color.FromArgb(30, 30, 50);
    }

    internal class Glib_core
    {
        private static readonly Glib_core Gcore;
        private static readonly SVGAIICanvas Function = Driver.Function;
        private static readonly int Sx = Driver.Width, Sy = Driver.Height;

        ///<summary>Draws a rounded box</summary>
        public void Rounded_Box(Color color, int x, int y, int Width, int Height, int radius)
        {
            int x2 = x + Width, y2 = y + Height, r2 = radius + radius;
            // Draw Outside circles
            Function.DrawFilledCircle(new Pen(color), x + radius, y + radius, radius);
            Function.DrawFilledCircle(new Pen(color), x2 - radius - 1, y + radius, radius);
            Function.DrawFilledCircle(new Pen(color), x + radius, y2 - radius - 1, radius);
            Function.DrawFilledCircle(new Pen(color), x2 - radius - 1, y2 - radius - 1, radius);

            // Draw Main Rectangle
            Function.DrawFilledRectangle(new Pen(color), x, y + radius, Width, Height - r2);
            // Draw Outside Rectangles
            Function.DrawFilledRectangle(new Pen(color), x + radius, y, Width - r2, radius);
            Function.DrawFilledRectangle(new Pen(color), x + radius, y2 - radius, Width - r2, radius);
        }

        ///<summary>Draws a rounded box (top)</summary>
        public void Top_Rounded_Box(Color color, int x, int y, int Width, int Height, int radius = 6)
        {
            int x2 = x + Width, r2 = radius + radius;
            // Draw Outside circles
            Function.DrawFilledCircle(new Pen(color), x + radius, y + radius, radius);
            Function.DrawFilledCircle(new Pen(color), x2 - radius - 1, y + radius, radius);

            // Draw Main Rectangle
            Function.DrawFilledRectangle(new Pen(color), x, y + radius, Width, Height - radius);
            // Draw Outside Rectangles
            Function.DrawFilledRectangle(new Pen(color), x + radius, y, Width - r2, radius + 3);
        }

        ///<summary>Draws a rounded box (bottom)</summary>
        public void Bottom_Rounded_Box(Color color, int x, int y, int Width, int Height, int radius = 6)
        {
            int x2 = x + Width, y2 = y + Height, r2 = radius + radius;
            // Draw Outside circles
            Function.DrawFilledCircle(new Pen(color), x + radius, y2 - radius - 1, radius);
            Function.DrawFilledCircle(new Pen(color), x2 - radius - 1, y2 - radius - 1, radius);

            // Draw Main Rectangle
            Function.DrawFilledRectangle(new Pen(color), x, y + radius, Width, Height - r2);
            // Draw Outside Rectangles
            Function.DrawFilledRectangle(new Pen(color), x + radius, y2 - radius, Width - r2, radius);
        }

        ///<summary>Draws a text string with an app defined font</summary>
        public void Text(Color color, string font, string text, int x, int y)
        {
            Function.DrawBitFontString(font, color, text, x, y);
        }

        ///<summary>(wip) Draw a loading bar with a specified amout of progress filled</summary>
        public void Loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Rounded_Box(Color.SlateGray, fromX, fromY, length, height, 50);
            Rounded_Box(Color.White, fromX - 1, fromY - 1, percentage + 2, height + 2, 50);
        }

        ///<summary>Draws a few boxes to get a window</summary>
        public void Window(string font, int from_X, int from_Y, int Width, int Height, string Title, bool showtitlebar, int radius)
        {
            Rounded_Box(Themes.Window.Window_Theme, from_X - 1, from_Y - 1, Width + 2, Height + 2, radius);
            Bottom_Rounded_Box(Themes.Window.Window_Theme_Inner, from_X, from_Y, Width, Height, radius);
            if (showtitlebar) { Top_Rounded_Box(Themes.Window.Window_Theme, from_X, from_Y, Width, Sy / 26, radius); }
            Text(Color.White, font, Title, from_X + 10, from_Y + 4);
        }

        ///<summary>Draws a text string with a block behind it</summary>
        public void Textbox(string font, string text, Color Background, Color Foreground, Color accent, int from_X, int from_Y, int Width)
        {
            Rounded_Box(accent, from_X - 1, from_Y - 1, Width + 2, 17, 2);
            Rounded_Box(Background, from_X, from_Y, Width, 15, 2);
            Text(Foreground, font, text, from_X, from_Y + 1);
        }

        ///<summary>Draws a bitmap image</summary>
        public void Image(Bitmap img, int x, int y)
        {
            Function.DrawImageAlpha(img, x, y);
        }
    }

    internal abstract class BaseGuiElement
    {
        public int X, Y, Width, Height;

        public BaseGuiElement Parent;

        public BaseGuiElement(int x, int y, int w = 0, int h = 0, BaseGuiElement parent = null)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Parent = parent;
        }

        internal abstract void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y);

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

    internal class GuiWindow : BaseGuiElement
    {
        public string Title;
        public List<BaseGuiElement> Children;

        private int lastX, lastY;
        private readonly int titleHeight;

        public GuiWindow(int x, int y, int w, int h, string title) : base(x, y, w, h)
        {
            Title = title;
            Children = new List<BaseGuiElement>();
            lastX = x;
            lastY = y;
            titleHeight = Driver.Height / 25;
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

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x = 0, int offset_y = 0)
        {
            if (lastX != X || lastY != Y) // we moved
            {
                Gcore.Rounded_Box(Themes.Desktop_main, lastX, lastY, Width, Height, 5);
                lastX = X;
                lastY = Y;
            }
            Gcore.Window(Driver.Font, X, Y, Width, Height, Title, true, 5);

            foreach (var child in Children)
            {
                child.Render(Gcore, Function, X, Y + titleHeight);
            }
        }
    }

    internal class GuiText : BaseGuiElement
    {
        public string Value;
        public Color TextColour;

        public GuiText(int x, int y, string text, Color textcolor) : base(x, y, 0, 0)
        {
            Value = text;
            TextColour = textcolor;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            Gcore.Text(TextColour, Driver.Font, Value, X + offset_x, Y + offset_y);
        }
    }

    internal class GuiTextBox : BaseGuiElement
    {
        public string Value;
        public Color Background;
        public Color TextColour;
        public Color Accent;

        public GuiTextBox(int x, int y, int w, int h, Color Theme, Color Inner, Color Text) : base(x, y, w, h)
        {
            Background = Inner;
            TextColour = Text;
            Accent = Theme;
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

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            string txt = Value + (Desktop.ActiveElement == this ? "|" : "");
            Gcore.Textbox(Driver.Font, txt, Background, TextColour, Accent, X + offset_x, Y + offset_y, Width);
        }
    }

    internal class GuiButton : BaseGuiElement
    {
        public string Value;
        public Color Background;
        public Color TextColour;

        public delegate void ClickDelegate(GuiButton self);

        public ClickDelegate OnClick;

        public GuiButton(int x, int y, int w, int h, string text, Color Textclr, Color Backcolor, ClickDelegate onClick) : base(x, y, w, h)
        {
            Value = text;
            Background = Backcolor;
            TextColour = Textclr;
            OnClick = onClick;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            int mx = X + 4;
            int my = Y + (Height / 2) - 8;
            Gcore.Rounded_Box(Background, X + offset_x - 1, Y + offset_y - 1, Width + 2, Height + 2, 3);
            Gcore.Rounded_Box(Themes.Button.Button_Theme_Inner, X + offset_x, Y + offset_y, Width, Height, 3);
            Gcore.Text(TextColour, Driver.Font, Value, mx + offset_x, my + offset_y);
        }

        internal override bool Click(int x, int y, int btn)
        {
            OnClick(this);
            return base.Click(x, y, btn);
        }
    }

    internal class GuiImage : BaseGuiElement
    {
        public Bitmap img;
        public bool showborder;
        public int xHeight;
        public int xWidth;

        public GuiImage(int x, int y, int w, int h, Bitmap image, bool border) : base(x, y, w, h)
        {
            X = x;
            Y = y;
            img = image;
            showborder = border;
            xHeight = h;
            xWidth = w;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            Gcore.Image(img, offset_x + X, offset_y + Y);
            if (showborder) { Driver.Function.DrawRectangle(new Pen(Color.Black), X, Y, xWidth, xHeight); }
        }
    }

    internal class GuiSwitch : BaseGuiElement
    {
        public Color Back;
        public Color Fore;
        public bool status;

        public delegate void ClickDelegate(GuiSwitch self);

        public ClickDelegate OnClick;

        public GuiSwitch(int x, int y, Color Background, Color Foreground, bool enabled, ClickDelegate onClick) : base(x, y, 45, 20)
        {
            Back = Background;
            Fore = Foreground;
            status = enabled;
            OnClick = onClick;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            Gcore.Rounded_Box(Fore, offset_x + X - 1, offset_y + Y - 1, 39, 23, 9);
            Gcore.Rounded_Box(Back, offset_x + X, offset_y + Y, 37, 21, 9);
            if (status) { Driver.Function.DrawFilledCircle(new Pen(Color.White), offset_x + X + 10, offset_y + Y + 10, 9); }
            if (!status) { Driver.Function.DrawFilledCircle(new Pen(Color.White), offset_x + X + 47, offset_y + Y + 10, 9); }
        }

        internal override bool Click(int x, int y, int btn)
        {
            OnClick(this);
            return base.Click(x, y, btn);
        }
    }

    internal class GuiDiv : BaseGuiElement
    {
        public Color Fore;

        public GuiDiv(int x, int y, int h, int w, Color Foreground) : base(x, y, h, w)
        {
            Fore = Foreground;
            Height = h;
            Width = w;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            Driver.Function.DrawLine(new Pen(Fore), offset_x + X, offset_y + Y, Width, offset_y + Y);
            Driver.Function.DrawLine(new Pen(Fore), offset_x + X, offset_y + Y + Height, Width, offset_y + Y + Height);
        }
    }

    internal class GuiToggle : BaseGuiElement
    {
        public Color Fore;
        public Color Back;
        public bool enable;

        public delegate void ClickDelegate(GuiToggle self);

        public ClickDelegate OnClick;

        public GuiToggle(int x, int y, Color Foreground, Color Background, bool state, ClickDelegate onClick) : base(x, y)
        {
            OnClick = onClick;
            Fore = Foreground;
            Back = Background;
            enable = state;
        }

        internal override void Render(Glib_core Gcore, Driver Function, int offset_x, int offset_y)
        {
            Gcore.Rounded_Box(Fore, offset_x + X - 1, offset_y + Y - 1, Driver.Width / 80 + 2, Driver.Height / 45 + 2, 6);
            Gcore.Rounded_Box(Back, offset_x + X, offset_y + Y, Driver.Width / 80, Driver.Height / 45, 6);
            if (enable) { Gcore.Rounded_Box(Fore, offset_x + X + 2, offset_y + Y + 2, Driver.Width / 80 - 4, Driver.Height / 45 - 4, 6); }
        }

        internal override bool Click(int x, int y, int btn)
        {
            OnClick(this);
            return base.Click(x, y, btn);
        }
    }
}