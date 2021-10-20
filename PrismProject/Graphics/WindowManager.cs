using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using static PrismProject.Graphics.Canvas2;
using static PrismProject.Services.Basic.Mouse_Service;
using Cosmos.System.Graphics.Fonts;

namespace PrismProject.Graphics
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
            public List<Element> Children = new List<Element>();

            public Window(int aX, int aY, int aWidth, int aHeight, string aTitle)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
                Title = aTitle;
            }

            public void Update()
            {
                Render();
                foreach (Element Child in Children)
                {
                    Child.OnClickAction();
                    Child.Render(X, Y);
                }
            }

            public void AddChild(Element aChild)
            {
                Children.Add(aChild);
            }

            public void Dispose()
            {
                throw new NotImplementedException("Disposing of windows is not implemented.");
            }

            public void Render()
            {
                int HalfX = Width / 2;
                int HalfY = Height / 2;
                DrawRoundRect(X - HalfX, Y - HalfY - PCScreenFont.Default.Height + 4, X + HalfX, Y + HalfY, 4, Color.DimGray);
                DrawRoundRect(X - HalfX, Y - HalfY, X + HalfX, Y + HalfY, 4, Color.White);
            }
        }

        public class Element
        {
            public virtual void Render(int Offset_X, int Offset_Y)
            {

            }
            public virtual bool IsClicked()
            {
                return false;
            }
            public virtual void OnClickAction()
            {
                throw new NotImplementedException("Element actions are not yet implemented.");
            }
        }

        public class Button : Element
        {
            public static int X;
            public static int Y;
            public static int Width;
            public static int Height;

            public Button(int aX, int aY, int aWidth, int aHeight)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
            }

            public override void Render(int Offset_X, int Offset_Y)
            {

            }
            public override bool IsClicked()
            {
                if (IsLeftClicked)
                {
                    if (MouseX < X && MouseX > Width)
                    {
                        if (Mousey < X && Mousey > Height)
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;

            }
            public override void OnClickAction()
            {
                if (IsClicked())
                {
                    base.OnClickAction();
                }
            }
        }

        public class Image : Element
        {
            public static Bitmap Bmp;
            public static int X;
            public static int Y;
            public Image(int aX, int aY, Bitmap aBmp)
            {
                X = aX - ((int)aBmp.Width / 2);
                Y = aY - ((int)aBmp.Height / 2);
                Bmp = aBmp;
            }
            public override void Render(int Offset_X, int Offset_Y)
            {
                Screen.DrawImageAlpha(Bmp, Offset_X + X, Offset_Y + Y);
            }
            public override bool IsClicked()
            {
                if (IsLeftClicked)
                {
                    if (MouseX < X && MouseX > Bmp.Width)
                    {
                        if (Mousey < X && Mousey > Bmp.Height)
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
                
            }
            public override void OnClickAction()
            {
                if (IsClicked())
                {
                    base.OnClickAction();
                }
            }
        }

        public class Text : Element
        {
            public int X;
            public int Y;
            public string TextString;
            public  Color color;
            public PCScreenFont Font;

            public Text(int aX, int aY, string aTextString, Color aColor, PCScreenFont aFont)
            {
                X = aX;
                Y = aY;
                TextString = aTextString;
                color = aColor;
                Font = aFont;
            }
            public override void Render(int Offset_X, int Offset_Y)
            {
                Screen.DrawString(TextString, Font, new Pen(color), Offset_X + X - (Font.Width * TextString.Length), Offset_Y + Y);
            }
            public override bool IsClicked()
            {
                return false;
            }
            public override void OnClickAction()
            {
                if (IsClicked())
                {
                    base.OnClickAction();
                }
            }
        }
    }
    
}
