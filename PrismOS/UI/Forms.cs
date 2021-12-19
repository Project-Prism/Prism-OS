using S = System.Drawing;
using System.Collections.Generic;
using Cosmos.System.Graphics;
using static PrismOS.UI.Framework;

namespace PrismOS.UI
{
    public static class Forms
    {
        public abstract class Form
        {
            #region Config
            // X, Y, W, H, R = X, Y, Width, Height and Radius.
            public int X;
            public int Y;
            public int W;
            public int H;
            public int R;

            // Extra properties
            public bool Visible;
            public string Text;
            public Theme Theme;

            // The parent and children for the form.
            public Window Parent;
            public List<Form> Children;

            // Click event for the form.
            public ClickDelegate OnClick;
            public delegate void ClickDelegate(Form This);
            #endregion Config

            public abstract void Draw();

            public abstract void Click();
        }

        public class Window : Form
        {
            public Window(int aX, int aY, int aW, int aH, int aR, string aText, Theme aTheme, bool aVisible)
            {
                X = aX;
                Y = aY;
                W = aW;
                H = aH;
                R = aR;
                Text = aText;
                Theme = aTheme;
                Children = new();
                Parent = null;
                Visible = aVisible;
            }

            #region Config
            // X, Y, W, H, R = X, Y, Width, Height and Radius.
            public new int X;
            public new int Y;
            public new int W;
            public new int H;
            public new int R;

            // Extra properties
            public new bool Visible;
            public new string Text;
            public new Theme Theme;

            // The parent and children for the form.
            public new Form Parent;
            public new List<Form> Children;

            // Click event for the form.
            public new ClickDelegate OnClick;
            public new delegate void ClickDelegate(Form This);
            #endregion Config

            public override void Draw()
            {
                if (Visible)
                {
                    Framework.Canvas.DrawFilledRectangle(new Pen(Theme.BackGround), X, Y, W, H);
                }
            }

            public override void Click()
            {
            }
        }

        public class Button : Form
        {
            public Button(int aX, int aY, int aW, int aH, int aR, string aText, Theme aTheme, Form aParent, bool aVisible)
            {
                X = aX;
                Y = aY;
                W = aW;
                H = aH;
                R = aR;
                Text = aText;
                Theme = aTheme;
                Children = new();
                Parent = aParent;
                Visible = aVisible;
            }

            #region Config
            // X, Y, W, H, R = X, Y, Width, Height and Radius.
            public new int X;
            public new int Y;
            public new int W;
            public new int H;
            public new int R;

            // Extra properties
            public new bool Visible;
            public new string Text;
            public new Theme Theme;

            // The parent and children for the form.
            public new Form Parent;
            public new List<Form> Children;

            // Click event for the form.
            public new ClickDelegate OnClick;
            public new delegate void ClickDelegate(Form This);
            #endregion Config

            public override void Draw()
            {
                if (Visible)
                {
                    Framework.Canvas.DrawFilledRectangle(new Pen(Theme.BackGround), Parent.X + X, Parent.Y + Y, W, H);
                }
            }

            public override void Click()
            {
                S.Color X = Theme.BackGround;
                Theme.BackGround = S.Color.White;
                OnClick.Invoke(this);
                Theme.BackGround = X;
            }
        }
    }
}
