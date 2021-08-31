using Cosmos.System;
using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;
using PrismProject.System2.Drivers;
using PrismProject.System2.Extra;
using System;

namespace PrismProject.System2.Drawing
{
    /// <summary>
    /// WinLib window manager, made by CrisisSDK.
    /// </summary>
    internal abstract class WinLib
    {
        //UI elements
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

            internal abstract void Render(UILib drawing, int offset_x, int offset_y);

            internal virtual bool Click(int x, int y, int btn)
            {
                Software.Screen0.ActiveElement = this;
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
                titleHeight = Video.SH / 25;
            }

            public void AddChild(BaseGuiElement guiElement)
            {
                guiElement.Parent = this;
                Children.Add(guiElement);
            }

            internal override bool Click(int x, int y, int btn)
            {
                Software.Screen0.ActiveElement = this;
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

            internal override void Render(UILib drawing, int offset_x = 0, int offset_y = 0)
            {
                if (lastX != X || lastY != Y) // we moved
                {
                    drawing.DrawRoundedBox(lastX, lastY, Width, Height, Properties.WMProperties.DefaultRadius(), Themes.desktop, true, true);
                    lastX = X;
                    lastY = Y;
                }
                drawing.DrawBlankWindow(X, Y, Width, Height, Title, 4);

                foreach (var child in Children)
                {
                    child.Render(drawing, X, Y + titleHeight);
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawText(offset_x+X, offset_y+Y, TextColour, Video.Font, Value);
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                string txt = Value + (Software.Screen0.ActiveElement == this ? "|" : "");
                drawing.DrawTextbox(offset_x+X, offset_y+Y, Width, 4, Video.Font, txt, Themes.Textbox.YB_Inner, Themes.Textbox.TB_Border, Themes.Window.WindowTextColor);
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawRoundedBox(offset_x+X-1, offset_y+Y-1, Width+2, Height+2, 3, Background, true, true);
                drawing.DrawRoundedBox(offset_x+X, offset_y+Y, Width, Height, 3, Themes.Button.Button_Color, true, true);
                drawing.DrawText(X+4+offset_x, Y+(Height/2)-8+offset_y, TextColour, Video.Font, Value);
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawImage(img, offset_x + X, offset_y + Y);
                if (showborder) { Video.Screen.DrawRectangle(new Pen(Color.Black), X, Y, xWidth, xHeight); }
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawRoundedBox(offset_x+X-1, offset_y+Y-1, 39, 23, 9, Themes.Switch.Switch_Theme, true, true);
                drawing.DrawRoundedBox(offset_x+X, offset_y+Y, 37, 21, 9, Back, true, true);
                if (status) { Video.Screen.DrawFilledCircle(new Pen(Color.White), offset_x + X + 10, offset_y + Y + 10, 9); }
                if (!status) { Video.Screen.DrawFilledCircle(new Pen(Color.White), offset_x + X + 47, offset_y + Y + 10, 9); }
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawLine(offset_x+X, offset_y+Y, Width, 5, Fore);
                drawing.DrawLine(offset_x+X, offset_y+Y+Height, Width, 5, Fore);
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

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                drawing.DrawRoundedBox(offset_x+X-1, offset_y+Y-1, Video.SW/80+2, Video.SH/45+2, 6, Fore, true, true);
                drawing.DrawRoundedBox(offset_x+X, offset_y+Y, Video.SW/80, Video.SH/45, 6, Back, true, true);
                if (enable) { drawing.DrawRoundedBox(offset_x+X+2, offset_y+Y+2, Video.SW/80-4, Video.SH/45-4, 6, Fore, true, true); }
            }

            internal override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

        //Icons drawing
        internal class GuiIcon : BaseGuiElement
        {
            public Color clr;
            public string nme;
            public int W, H;

            public GuiIcon(int x, int y, int w, int h, string name, Color color) : base(x, y, w, h)
            {
                nme = name;
                clr = color;
                X = x;
                Y = y;
                H = h;
                W = w;
            }

            internal override void Render(UILib drawing, int offset_x, int offset_y)
            {
                Icons.Icons.DrawIcon(X+offset_x, Y+offset_y, W, H, clr, nme);
            }
        }
    }
}
