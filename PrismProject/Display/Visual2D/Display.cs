using System;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using static PrismProject.Display.Visual2D.DisplayConfig;


namespace PrismProject.Display.Visual2D
{
    class Display
    {
        public class Basic
        {
            public static void DrawRect(int X1, int Y1, int X2, int Y2, Color C, bool Filled)
            {
                switch (Filled)
                {
                    case true:
                        Controler.DrawFilledRectangle(new Pen(C), X1, Y1, X2, Y2);
                        break;
                    case false:
                        Controler.DrawRectangle(new Pen(C), X1, Y1, X2, Y2);
                        break;
                }
            }

            public static void DrawRoundRect(int X1, int Y1, int X2, int Y2, int R, Color C)
            {
                int x2 = X1 + X2, y2 = Y1 + Y2, r2 = R + R;
                DrawRect(X1, Y1 + R, X2, Y2 - r2, C, true); // Left Right
                DrawRect(X1 + R, Y1, X2 - r2, R, C, true); //Bottom Bar
                DrawRect(X1 + R, y2 - R, X2 - r2, R, C, true); //Top Bar

                DrawCirc(X1 + R, Y1 + R, R, C, true); // Top left
                DrawCirc(x2 - R - 1, Y1 + R, R, C, true); // Top Right
                DrawCirc(X1 + R, y2 - R - 1, R, C, true); // Bottom Left
                DrawCirc(x2 - R - 1, y2 - R - 1, R, C, true);
            } // Bottom Right

            public static void DrawCirc(int X1, int Y1, int R, Color C, bool filled)
            {
                switch (filled)
                {
                    case true:
                        Controler.DrawFilledCircle(new Pen(C), X1, Y1, R);
                        break;
                    case false:
                        Controler.DrawCircle(new Pen(C), X1, Y1, R);
                        break;
                }
            }

            public static void DrawLine(int X1, int Y1, int X2, int Y2, Color C)
            {
                Controler.DrawLine(new Pen(C), X1, Y1, X2, Y2);
            }

            public static void DrawAngle(int X, int Y, int Angle, int Radius, Color color)
            {
                Double angleX, angleY;
                angleY = Radius * Math.Cos(Math.PI * 2 * Angle / 360);
                angleX = Radius * Math.Sin(Math.PI * 2 * Angle / 360);
                DrawLine(X, Y, X + (int)(Math.Round(angleX * 100) / 100), Y - (int)(Math.Round(angleY * 100) / 100), color);
            }

            public static void DrawString(string Text, PCScreenFont Font, Color c, int X, int Y, AnchorPoint anc)
            {
                string[] Txt = Text.Split("\n");
                int Space = 0;
                int NewX = 0;
                int NewY = 0;
                foreach (string line in Txt)
                {
                    if (anc == AnchorPoint.TopLeft)
                    {
                        NewX = X;
                        NewY = Y;
                    }
                    if (anc == AnchorPoint.Center)
                    {
                        NewX = X - (Font.Width / 2 * line.Length);
                        NewY = Y - (Font.Height / 2 * Txt.Length);
                    }

                    Controler.DrawString(line, Font, new Pen(c), NewX, NewY + Space);
                    Space += Font.Height + 5;
                }
            }

            public static void DrawBMP(int X, int Y, Bitmap BMP, AnchorPoint anc)
            {
                int NewX = 0;
                int NewY = 0;
                if (anc == AnchorPoint.Center)
                {
                    NewX = X - (int)BMP.Width / 2;
                    NewY = Y - (int)BMP.Height / 2;
                }
                if (anc == AnchorPoint.TopLeft)
                {
                    NewX = X;
                    NewY = Y;
                }

                Controler.DrawImageAlpha(BMP, NewX, NewY);
            }
        }

        public class Elements
        {
            public struct Object // Make new object for each element
            {
                public int X;
                public int Y;
                public int Width;
                public int Height;
                public string[] Extra;
                public int BorderWidth;
                public int BorderRadius;
                public PCScreenFont Font;
                public Color[] Theme;
                public AnchorPoint Anchor;

                public void Dispose()
                {
                    X = 0;
                    Y = 0;
                    Width = 0;
                    Height = 0;
                    Extra = null;
                    BorderWidth = 0;
                    BorderRadius = 0;
                    Font = null;
                    Theme = null;
                }

                public Object(int aX, int aY, int aWidth, int aHeight, string[] aExtra, int aBorderWidth, int aBorderRadius, Color[] aTheme, PCScreenFont aFont, AnchorPoint aAnchor)
                {
                    X = aX;
                    Y = aY;
                    Width = aWidth;
                    Height = aHeight;
                    Extra = aExtra;
                    BorderRadius = aBorderRadius;
                    BorderWidth = aBorderWidth;
                    Theme = aTheme;
                    Font = aFont;
                    Anchor = aAnchor;
                }
            }

            public static void DrawButton(Object @object)
            {
                int X = @object.X - (@object.Width / 2);
                int Y = @object.Y - (@object.Height / 2);
                int Width = (@object.Width);
                int Height = (@object.Height);
                int Border = @object.BorderWidth;
                if (Width < (@object.Font.Width * @object.Extra[0].Length)) // Check if text is longer than button
                {
                    // Set button length to be longer than text
                    Width += (@object.Font.Width * @object.Extra[0].Length) + 10;
                }

                Basic.DrawRoundRect(
                    X1: X - Border,
                    Y1: Y - Border,
                    X2: Width + Border * 2,
                    Y2: Height + Border * 2,
                    R: @object.BorderRadius,
                    C: @object.Theme[0]);

                Basic.DrawRoundRect(
                    X1: X,
                    Y1: Y,
                    X2: Width,
                    Y2: Height,
                    R: @object.BorderRadius,
                    C: @object.Theme[1]);

                Basic.DrawString(
                    Text: @object.Extra[0],
                    Font: PCScreenFont.Default,
                    c: @object.Theme[2],
                    X: @object.X,
                    Y: @object.Y,
                    anc: @object.Anchor);
            }

            public static void DrawWindow(Object @object)
            {
                int X = @object.X - (@object.Width / 2);
                int Y = @object.Y - (@object.Height / 2);
                int Width = @object.Width;
                int Height = @object.Height;
                int Border = @object.BorderWidth;


                Basic.DrawRoundRect( // title bar and border
                    X1: X - Border,
                    Y1: Y - Border - @object.Font.Height + 2,
                    X2: Width + Border,
                    Y2: Height + Border,
                    R: @object.BorderRadius,
                    C: @object.Theme[0]);

                Basic.DrawRoundRect( // Content area
                    X1: X,
                    Y1: Y,
                    X2: Width,
                    Y2: Height,
                    R: @object.BorderRadius,
                    C: @object.Theme[1]);
                Basic.DrawString(
                    Text: @object.Extra[0],
                    Font: @object.Font,
                    c: @object.Theme[3],
                    X: @object.X,
                    Y: Y + (@object.Font.Height / 2),
                    anc: @object.Anchor);
            }

            public static void DrawMessageBox(Object @object)
            {
                int X = @object.X - (@object.Width / 2);
                int Y = @object.Y - (@object.Height / 2);
                int Width = @object.Width;
                int Height = @object.Height;
                int Border = @object.BorderWidth;

                Basic.DrawRoundRect(
                    X1: X - Border,
                    Y1: Y - (Border + @object.Font.Height),
                    X2: Width + Border,
                    Y2: Height + Border,
                    R: @object.BorderRadius,
                    C: @object.Theme[0]); // Border
                Basic.DrawRoundRect(
                    X1: X,
                    Y1: Y,
                    X2: Width,
                    Y2: Height,
                    R: @object.BorderRadius,
                    C: @object.Theme[1]); // Content
                Basic.DrawString(
                    Text: @object.Extra[0], // Title text
                    Font: @object.Font,
                    c: @object.Theme[3], // Title text theme
                    X: @object.X,
                    Y: Y + (@object.Font.Height / 2) + 2,
                    anc: AnchorPoint.Center);

            }
        }
    }
}