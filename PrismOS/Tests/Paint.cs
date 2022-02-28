using PrismOS.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Mouse = Cosmos.System.MouseManager;

namespace PrismOS.Tests
{
    public class Paint
    {
        public class Circle
        {
            public Circle(int X, int Y, int Radius, Color C)
            {
                this.X = X;
                this.Y = Y;
                this.Radius = Radius;
                this.C = C;
            }

            public int X, Y, Radius;
            public Color C;
        }

        private readonly Random R = new();
        private Color C = Color.White;
        private readonly List<Color> Colors = new()
        {
            { Color.White },
            { Color.Red },
            { Color.Orange },
            { Color.Green },
            { Color.Blue },
            { Color.Purple },
            { Color.Pink },
        };
        private readonly List<Circle> Circles = new();
        private readonly List<Point> Points = new();

        public void Clock(Canvas Ca)
        {
            if (Mouse.MouseState == Cosmos.System.MouseState.Left)
            {
                Ca.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Red);
                Circles.Add(new((int)Mouse.X, (int)Mouse.Y, 2, C));
            }
            if (Mouse.MouseState == Cosmos.System.MouseState.Right)
            {
                Ca.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Blue);
                Circles.Clear();
            }
            if (Mouse.MouseState == Cosmos.System.MouseState.Middle)
            {
                C = Colors[R.Next(0, Colors.Count)];
            }
            foreach (Circle Cx in Circles)
            {
                Ca.DrawFilledCircle(Cx.X, Cx.Y, Cx.Radius, Cx.C);
            }
            Ca.DrawString(15, 15, "Left click: Create circle\nright click: Clear circles\nMid click: change color\n\nColor selected: " + C.Name, Color.White);
        }

        public void Clock2(Canvas Ca)
        {
            if (Mouse.MouseState == Cosmos.System.MouseState.Left)
            {
                Ca.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Red);
                Points.Add(new((int)Mouse.X, (int)Mouse.Y));
            }
            if (Mouse.MouseState == Cosmos.System.MouseState.Right)
            {
                Ca.DrawCircle((int)Mouse.X, (int)Mouse.Y, 15, Color.Blue);
                Points.Clear();
            }
            foreach (Point P in Points)
            {
                foreach (Point P2 in Points)
                {
                    Ca.DrawLine(P.X, P.Y, P2.X, P2.Y, Color.White);
                }
            }
            Ca.DrawString(15, 15, "Left click: Create point\nright click: Clear points", Color.White);
        }
    }
}