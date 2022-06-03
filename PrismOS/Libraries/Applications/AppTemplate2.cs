using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Graphics.GUI;
using PrismOS.Libraries.Numerics;
using System.Collections.Generic;
using static Cosmos.System.MouseManager;

namespace PrismOS.Libraries.Applications
{
    public class AppTemplate2 : Runtime.Application
    {
        public Color Color;
        public bool ColorSet = false;
        public Panel Panel1 = new();
        public Window Window = new();
        public Textbox Textbox1 = new();
        public List<(Color, Position)> Points = new();

        public override void OnCreate()
        {
            // Main window
            Window.Position = new(256, 256);
            Window.Size = new(400, 175);
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Form1";

            Textbox1.Position = new(49, 12);
            Textbox1.Size = new(150, 31);
            Textbox1.Hint = "Hex value";
            Window.Elements.Add(Textbox1);

            Runtime.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {
            Canvas.Current.DrawFilledRectangle(12, 12, 31, 31, 0, Color);
            foreach ((Color, Position) P1 in Points)
            {
                foreach ((Color, Position) P2 in Points)
                {
                    Canvas.Current.DrawLine(P1.Item2.X, P1.Item2.Y, P2.Item2.X, P2.Item2.Y, P1.Item1);
                }
            }
            if (MouseState == Cosmos.System.MouseState.Left && ColorSet && Runtime.IsMouseWithin(Window.Position.X, Window.Position.Y, Window.Size.Width, Window.Size.Height))
            {
                Points.Add((Color, new((int)X, (int)Y)));
            }
            try
            {
                Color.ARGB = System.Convert.ToUInt32(Textbox1.Text, 16);
                ColorSet = true;
            }
            catch
            {
                ColorSet = false;
            }
        }
    }
}