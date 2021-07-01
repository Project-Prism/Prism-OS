using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace PrismProject
{
    class Elements
    {
        //Define the graphics method
        public static int screenX = Driver.screenX;
        public static int screenY = Driver.screenY;
        public static Canvas canvas = Driver.canvas;
        public static Elements draw = new Elements();
        public static Cursor cursor = new Cursor();
        public static int Spacing = 0;

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
        public void Line(Color color, int from_X, int from_y, int to_X, int to_Y)
        {
            canvas.DrawLine(new Pen(color), from_X, from_y, to_X, to_Y);
        }
        public void Text(String text, Color color, int Width, int x, int y)
        {
            char[] Char_array = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                Char_array[i] = text[i];
            }
            int space = 0;
            foreach (char c in Char_array)
            {
                int Margin = space + Width + 5;
                canvas.DrawChar(c, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Pen(color), x+Margin, y);
                space++;
                space++;
                space++;
                space++;
                space++;
                space++;
                space++;
                space++;
                space++;
                space++;
            }
            
        }
        public void Arrow(Color color, int width, int x, int y)
        {
            canvas.DrawPoint(new Pen(color, width), x, y);
        }
        public void Array(Color[] Colors_array, Cosmos.System.Graphics.Point point, int Width, int Height)
        {
            canvas.DrawArray(Colors_array, point, Width, Height); 
        }
        public void Loadbar(int fromX, int fromY, int length, int height, int percentage)
        {
            Box(Color.DarkGray, Convert.ToInt32(fromX), Convert.ToInt32(fromY), length, height);
            Box(Color.White, Convert.ToInt32(fromX), Convert.ToInt32(fromY), percentage, height);
        }
        public void Window(int from_X, int from_Y, int Width, int Height, string Title)
        {
            Box(Desktop.Window, from_X, from_Y, Width, Height);
            Box(Desktop.Windowbar, from_X, from_Y, Width, screenY / 25);
            Text(Title, Color.White, 15, from_X, from_Y);
        }
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
