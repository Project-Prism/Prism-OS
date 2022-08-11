using PrismOS.Libraries.Rasterizer.Objects;
using PrismOS.Libraries.Rasterizer;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.UI;
using Cosmos.System;

namespace PrismOS.Apps
{
    public unsafe class AppTemplate1 : Window
    {
        public Button Button = new();
        public Cube C2 = new(300, 1, 300);
        public Cube C1 = new(150, 50, 150);
        public Image Image1 = new();
        public Engine E;

        public AppTemplate1()
        {
            // Main window
            X = 50;
            Y = 50;
            Width = 600;
            Height = 300;
            Text = "3D Testing";
            HasBorder = true;

            // Image1
            Image1.X = 0;
            Image1.Y = 20;
            Image1.Width = Width;
            Image1.Height = Height - 20;

            // Button
            Button.X = (int)(Width - 20);
            Button.Width = 20;
            Button.Height = 20;
            Button.Text = "X";
            Button.HasBorder = true;
            Button.OnClickEvents.Add(() => { Windows.Remove(this); });

            // Engine1
            E = new(Image1.Width, Image1.Height, 45);
            C2.Position = new(0, 50, 0);
            E.Objects.Add(C1);
            E.Objects.Add(C2);

            Elements.Add(Button);
            Elements.Add(Image1);
            Windows.Add(this);
        }

        public override void OnDraw(FrameBuffer buffer)
        {
            base.OnDraw(buffer);

            E.Render(Image1.Buffer);
            C1.TestLogic(-0.01);
            C2.TestLogic(0.05);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
            base.OnKeyPress(Key);

            switch (Key.Key)
            {
                case ConsoleKeyEx.W:
                    E.Camera.Position.Z -= 5;
                    break;
                case ConsoleKeyEx.S:
                    E.Camera.Position.Z += 5;
                    break;
                case ConsoleKeyEx.A:
                    E.Camera.Position.X -= 5;
                    break;
                case ConsoleKeyEx.D:
                    E.Camera.Position.X += 5;
                    break;
                case ConsoleKeyEx.E:
                    E.Camera.Position.Y += 5;
                    break;
                case ConsoleKeyEx.Q:
                    E.Camera.Position.Y -= 5;
                    break;
            }
        }
    }
}