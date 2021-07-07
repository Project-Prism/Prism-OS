using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject
{
    class Elements
    {
        //Define the graphics method
        public static int screenX = Driver.screenX, screenY = Driver.screenY, Spacing = 0;
        public static Canvas canvas = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();

        //Individual shapes
        public void Box(Color color, int from_X, int from_Y, int Width, int Height)
        {
            canvas.DrawFilledRectangle(new Pen(color), from_X, from_Y, Width, Height);
        }
        public void Empty_Box(Color color, int from_X, int from_Y, int to_X, int to_Y)
        {
            canvas.DrawRectangle(new Pen(color), from_X, from_Y, to_X, to_Y);
        }
        public void Circle(Color color, int from_X, int from_Y, int radius)
        {
            canvas.DrawFilledCircle(new Pen(color), from_X, from_Y, radius);
        }
        public void Triangle(Color color, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            canvas.DrawTriangle(new Pen(color), x1, y1, x2, y2, x3, y3);
        }
        public void Arrow(Color color, int width, int x, int y)
        {
            canvas.DrawPoint(new Pen(color, width), x, y);
        }
        public void Line(Color color, int from_X, int from_y, int to_X, int to_Y)
        {
            canvas.DrawLine(new Pen(color), from_X, from_y, to_X, to_Y);
        }

        //UI elements
        public void Text(String font, String text, Color color, int x, int y)
        {
            canvas.DrawBitFontString(font, color, text, x, y);

        }
        public void Array(Color[] Colors_array, Cosmos.System.Graphics.Point point, int Width, int Height)
        {
            canvas.DrawArray(Colors_array, point, Width, Height);
        }
        public void Loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Box(Color.SlateGray, Convert.ToInt32(fromX), Convert.ToInt32(fromY), length, height);
            Box(Color.White, Convert.ToInt32(fromX), Convert.ToInt32(fromY), percentage, height);
        }
        public void Window(string font, int from_X, int from_Y, int Width, int Height, string Title)
        {
            Box(Desktop.Window, from_X, from_Y, Width, Height);
            Box(Desktop.Windowbar, from_X, from_Y, Width, screenY / 25);
            Text(font, Title, Color.White, from_X, from_Y+5);
        }
        public void Textbox(string font, String text, Color Background, Color Foreground, int from_X, int from_Y, int Width)
        {
            draw.Box(Background, from_X, from_Y, Width, 15);
            draw.Text(font, text, Foreground, from_X, from_Y + 1);
        }
        public void Image(Bitmap img, int x, int y)
        {
            canvas.DrawImageAlpha(img, x, y);
        }

        //other
        public void Clear(Color color)
        {
            canvas.Clear(color);
        }
        public static void Exit()
        {
            if (Kernel.canvasRunning)
            {
                Kernel.canvasRunning = false;
                canvas.Disable();
            }
        }
    }
}
