using PrismGL3D.Objects;
using PrismGL2D.UI;
using PrismGL2D;
using PrismGL3D;
using System;

namespace PrismOS.Apps
{
    public unsafe class AppTemplate1 : Frame
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
            Button.OnClickEvents.Add((int X, int Y, Cosmos.System.MouseState State) => { Close(); });

            // Engine1
            E = new(Image1.Width, Image1.Height, 45);
            C2.Position = new(0, 50, 0);
            E.Objects.Add(C1);
            E.Objects.Add(C2);

            Controls.Add(Button);
            Controls.Add(Image1);
            Frames.Add(this);
        }

        public override void OnDrawEvent(Graphics buffer)
        {
            base.OnDrawEvent(buffer);

            E.Render(Image1.Source);
            C1.TestLogic(-0.01);
            C2.TestLogic(0.05);
        }

        public override void OnKeyEvent(ConsoleKeyInfo Key)
        {
            base.OnKeyEvent(Key);

            switch (Key.Key)
            {
                case ConsoleKey.W:
                    E.Camera.Position.Z -= 5;
                    break;
                case ConsoleKey.S:
                    E.Camera.Position.Z += 5;
                    break;
                case ConsoleKey.A:
                    E.Camera.Position.X -= 5;
                    break;
                case ConsoleKey.D:
                    E.Camera.Position.X += 5;
                    break;
                case ConsoleKey.E:
                    E.Camera.Position.Y += 5;
                    break;
                case ConsoleKey.Q:
                    E.Camera.Position.Y -= 5;
                    break;
            }
        }
    }
}