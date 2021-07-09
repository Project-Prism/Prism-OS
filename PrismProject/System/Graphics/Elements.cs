using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject
{
    class Elements
    {
        //Define the graphics method
        private static int screenX = Driver.screenX;
        private static int screenY = Driver.screenY;
        private static Canvas canvas = Driver.canvas;
        private static Elements draw = new Elements();

        //Individual shapes
        public void Box(Color color, int from_X, int from_Y, int Width, int Height)
        {
            canvas.DrawFilledRectangle(new Pen(color), from_X, from_Y, Width, Height);
        }
        public void Rounded_Box(Color color, int x, int y, int Width, int Height, int radius = 6)
        {
            int x2 = x+Width, y2 = y+Height, r2 = radius+radius;
            // Draw Outside circles
            draw.Circle(color, x+radius, y+radius, radius);
            draw.Circle(color, x2-radius-1, y+radius, radius);
            draw.Circle(color, x+radius, y2-radius-1, radius);
            draw.Circle(color, x2-radius-1, y2-radius-1, radius);

            // Draw Main Rectangle
            draw.Box(color, x, y+radius, Width, Height-r2);
            // Draw Outside Rectangles
            draw.Box(color, x+radius, y, Width-r2, radius);
            draw.Box(color, x+radius, y2-radius, Width-r2, radius);
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
        public void Text(Color color, String font, String text, int x, int y)
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
            Rounded_Box(Desktop.Window, from_X, from_Y, Width, Height);
            Rounded_Box(Desktop.Windowbar, from_X, from_Y, Width, screenY / 25);
            Text(Color.White, font, Title, from_X, from_Y+4);
        }
        public void Textbox(string font, string text, Color Background, Color Foreground, int from_X, int from_Y, int Width)
        {
            draw.Box(Background, from_X, from_Y, Width, 15);
            draw.Text(Foreground, font, text, from_X, from_Y + 1);
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
