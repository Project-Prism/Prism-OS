using PrismOS.Libraries.Graphics.Types;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.UI;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class AppTemplate1 : Application
    {
        public Window Window = new();
        public Button Button = new();
        public Shape.Cube C2 = new(300, 1, 300);
        public Shape.Cube C1 = new(150, 50, 150);
        public Image Image1 = new();
        public Engine E;

        public override void OnCreate()
        {
            // Main window
            Window.X = 50;
            Window.Y = 50;
            Window.Width = 600;
            Window.Height = 300;
            Window.Theme = Theme.DefaultDark;
            Window.Text = "3D Testing";

            // Image1
            Image1.X = 0;
            Image1.Y = 20;
            Image1.Width = Window.Width;
            Image1.Height = Window.Height - 20;

            // Button
            Button.X = Window.Width - 20;
            Button.Width = 20;
            Button.Height = 20;
            Button.Text = "X";
            Button.OnClick = new(() => { Window.Windows.Remove(Window); Applications.Remove(this); });

            // Engine1
            E = new((uint)Image1.Width, (uint)Image1.Height, 45);
            C2.Position = new(0, 50, 0);
            E.Objects.Add(C1);
            E.Objects.Add(C2);

            Window.Elements.Add(Button);
            Window.Elements.Add(Image1);
            Window.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {
            E.Render(Image1.FrameBuffer);
            C1.TestLogic(-0.01);
            C2.TestLogic(0.05);
        }
    }
}