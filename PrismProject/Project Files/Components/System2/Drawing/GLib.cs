using Cosmos.System.Graphics;
using PrismProject.System2.proprietary;
using System.Drawing;

namespace PrismProject.System2.Drawing
{
    class GLib
    {
            private static readonly SVGAIICanvas Screen = Drivers.Video.Screen;

            ///<summary>Clear screen</summary>
            public void ClearScreen(Color color)
            {
                Screen.Clear(color);
            }

            ///<summary>Draws a rounded box. Top and Bottom allow you to select wich sides get rounded.</summary>
            public void DrawRoundedBox(int x, int y, int W, int H, int R, Color color, bool Top, bool Bottom)
            {
            int x2 = x + W, y2 = y + H, r2 = R + R;
            if (Top && Bottom)
            {
                // Draw Outside circles
                Screen.DrawFilledCircle(new Pen(color), x + R, y + R, R);
                Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y + R, R);
                Screen.DrawFilledCircle(new Pen(color), x + R, y2 - R - 1, R);
                Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y2 - R - 1, R);
                // Draw Main Rectangle
                Screen.DrawFilledRectangle(new Pen(color), x, y + R, W, H - r2);
                // Draw Outside Rectangles
                Screen.DrawFilledRectangle(new Pen(color), x + R, y, W - r2, R);
                Screen.DrawFilledRectangle(new Pen(color), x + R, y2 - R, W - r2, R);
            }

            if (Top)
            {
                Screen.DrawFilledCircle(new Pen(color), x + R, y + R, R);
                Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y + R, R);
                // Draw Main Rectangle
                Screen.DrawFilledRectangle(new Pen(color), x, y + R, W, H - R);
                // Draw Outside Rectangles
                Screen.DrawFilledRectangle(new Pen(color), x + R, y, W - r2, R + 3);
            }  

            if (Bottom)
            {
                // Draw Outside circles
                Screen.DrawFilledCircle(new Pen(color), x + R, y2 - R - 1, R);
                Screen.DrawFilledCircle(new Pen(color), x2 - R - 1, y2 - R - 1, R);
                // Draw Main Rectangle
                Screen.DrawFilledRectangle(new Pen(color), x, y + R, W, H - r2);
                // Draw Outside Rectangles
                Screen.DrawFilledRectangle(new Pen(color), x + R, y2 - R, W - r2, R);
            }
        }

            ///<summary>Draws a text string with an app defined font</summary>
            public void DrawText(int x, int y, Color color, string font, string text)
            {
                Screen.DrawBitFontString(font, color, text, x, y);
            }

            ///<summary>(wip) Draw a loading bar with a specified amout of progress filled</summary>
            public void DrawProgressBar(int X, int Y, int W, int H, int Percent)
            {
                DrawRoundedBox(X, Y, W, H, 50, Color.SlateGray, true, true);
                DrawRoundedBox(X, Y, W/Percent, H, 50, Color.White, true, true);
            }

            ///<summary>Draws a few boxes to get a window</summary>
            public void DrawBlankWindow(int X, int Y, int W, int H, string Title, int R)
            {
                DrawRoundedBox(X-1, Y-1, W-2, H-2, R, Themes.Window.WindowSplash, true, true);
                DrawRoundedBox(X, Y, W, H, R, Themes.Window.WindowSplash, false, true);
                DrawText(X+10, Y+10, Color.White, Drivers.Video.Font, Title);
            }

            ///<summary>Draws a text string with a block behind it</summary>
            public void DrawTextbox(int X, int Y, int W, int R, string Font, string Text, Color BoxColor, Color BoxShadowColor, Color TextColor)
            {
                DrawRoundedBox(X-1, Y-1, W+2, 17, R, BoxShadowColor, true, true);
                DrawRoundedBox(X, Y, W, 16, R, BoxColor, true, true);
                DrawText(X, Y+1, TextColor, Font, Text);
            }

            ///<summary>Draws a bitmap image</summary>
            public void DrawImage(Bitmap img, int x, int y)
            {
                Screen.DrawImageAlpha(img, x, y);
            }

            public void DrawLine(int X, int Y, int Width, int Spacing, Color color)
            {
            GLib.Screen.DrawLine(new Pen(color), X, Y, Width, X);
            GLib.Screen.DrawLine(new Pen(color), X+Spacing, Y, Width, X+Spacing);
        }
        }
}
