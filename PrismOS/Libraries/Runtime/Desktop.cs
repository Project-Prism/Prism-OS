using PrismOS.Libraries.UI;
using System;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class Desktop : Application
    {
        public Window Window = new();
        public Button Button1 = new();

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
            Window.Draggable = false;

            // Button1
            Button1.X = 0;
            Button1.Y = 0;
            Button1.Width = 32;
            Button1.Height = 32;
            Button1.OnClick = new Action(() => { _ = new AppTemplate1(); });

            Window.Elements.Add(Button1);
            Window.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnKey(Cosmos.System.KeyEvent Key)
        {
        }
    }
}