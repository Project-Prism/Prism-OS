using PrismOS.Libraries.UI;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class AppTemplate1 : Application
    {
        public Window Window = new();
        public Label Label1 = new();

        public override void OnCreate()
        {
            // Main window
            Window.X = 50;
            Window.Y = 50;
            Window.Width = 200;
            Window.Height = 200;
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Performance Monitor";

            // Label1
            Label1.X = 12;
            Label1.Y = 29;
            Label1.Width = 38;
            Label1.Height = 15;
            Label1.Text = "";

            Window.Elements.Add(Label1);
            Window.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {
            Label1.Text = $"FPS: {Kernel.FPS}";
        }
    }
}