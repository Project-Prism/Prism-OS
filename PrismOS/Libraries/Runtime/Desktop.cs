using PrismOS.Libraries.UI;
using System;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class Desktop : Application
    {
        public Window Window = new();
        public Button Button = new();

        public override void OnCreate()
        {
            // Main window
            Window.X = 0;
            Window.Y = (int)(Kernel.Canvas.Height - 32);
            Window.Width = (int)Kernel.Canvas.Width;
            Window.Height = 32;
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Desktop";
            Window.TitleVisible = false;

            // Button
            Button.X = 0;
            Button.Y = 0;
            Button.Width = 32;
            Button.Height = 32;
            Button.OnClick = new Action(() => { ButtonClick(); });

            Window.Elements.Add(Button);
            Window.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {

        }

        public void ButtonClick()
        {
            _ = new AppTemplate1();
        }
    }
}