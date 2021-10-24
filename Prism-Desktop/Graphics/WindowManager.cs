using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;
using static Prism.Graphics.DBACanvas;
using Cosmos.System.Graphics.Fonts;

namespace Prism.Graphics
{
    class WindowManager
    {
        public class Window
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public string Title;
            public List<Element> Children = new();

            public Window(int aX, int aY, int aWidth, int aHeight, string aTitle)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Title = aTitle;
            }

            public void Render()
            {
                int HalfX = Width / 2;
                int HalfY = Height / 2;
                DrawRoundRect(X - HalfX, Y - HalfY - PCScreenFont.Default.Height + 4, X + HalfX, Y + HalfY, 4, Color.DimGray);
                DrawRoundRect(X - HalfX, Y - HalfY, X + HalfX, Y + HalfY, 4, Color.White);

                foreach (Element Child in Children)
                {
                    Child.Render(X, Y);
                }
            }

            public void AddChild(Element aChild)
            {
                Children.Add(aChild);
            }

            public void RemoveChild(Element aChild)
            {
                Children.Remove(aChild);
            }
        }

        public class Element
        {
            public delegate void ClickDelegate(Element self);
            public ClickDelegate OnClick;

            public virtual void Render(int Offset_X, int Offset_Y)
            {

            }
            public virtual bool Click(int x, int y, int btn)
            {
                return false;
            }
        }

        public class Button : Element
        {
            #region Variables
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public string Label;
            public new delegate void ClickDelegate(Button self);
            public new ClickDelegate OnClick;
            public bool CanClick = true;
            #endregion Variables

            public Button(int aX, int aY, int aWidth, int aHeight, string aLabel, ClickDelegate aOnClick)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Label = aLabel;
                OnClick = aOnClick;
            }

            public override void Render(int Offset_X, int Offset_Y)
            {

            }
            public override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

        public class Image : Element
        {
            #region Variables
            public Bitmap xImage;
            public int X;
            public int Y;
            public new delegate void ClickDelegate(Image self);
            public new ClickDelegate OnClick;
            #endregion Variables

            public Image(int aX, int aY, Bitmap aImage, ClickDelegate aOnClick)
            {
                X = aX - ((int)aImage.Width / 2);
                Y = aY - ((int)aImage.Height / 2);
                xImage = aImage;
                OnClick = aOnClick;
            }

            public override void Render(int Offset_X, int Offset_Y)
            {
                DrawImageAlpha(Offset_X + X, Offset_Y + Y, xImage);
            }
            public override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }

        public class Text : Element
        {
            #region Variables
            public int X;
            public int Y;
            public string TextString;
            public Color color;
            public PCScreenFont Font;
            public new delegate void ClickDelegate(Text self);
            public new ClickDelegate OnClick;
            #endregion Variables

            public Text(int aX, int aY, string aTextString, Color aColor, PCScreenFont aFont, ClickDelegate aOnClick)
            {
                X = aX;
                Y = aY;
                TextString = aTextString;
                color = aColor;
                Font = aFont;
                OnClick = aOnClick;
            }

            public override void Render(int Offset_X, int Offset_Y)
            {
                DrawString(Offset_X + X - (Font.Width * TextString.Length), Offset_Y + Y, Font, TextString, color);
            }
            public override bool Click(int x, int y, int btn)
            {
                OnClick(this);
                return base.Click(x, y, btn);
            }
        }
    }
    
}
