using PrismOS.Libraries.UI;

namespace PrismOS.Libraries.Runtime
{
    public unsafe class AppTemplate1 : Application
    {
        public Button Button1 = new();
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

            // Button1
            Button1.X = Window.Width - 20;
            Button1.Y = 0;
            Button1.Width = 20;
            Button1.Height = 20;
            Button1.Text = "X";
            Button1.OnClick = new System.Action(() => { Window.Windows.Remove(Window); });

            Window.Elements.Add(Label1);
            Window.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {
            //string GPUName = new((sbyte*)Cosmos.Core.VBE.getControllerInfo().oemProductNamePtr);

            Label1.Text = $"FPS: {Kernel.FPS}\n"/* +
                $"CPU Speed: {Cosmos.Core.CPU.GetCPUCycleSpeed() / 1024}\n" +
                $"CPU Brand: {Cosmos.Core.CPU.GetCPUBrandString()}\n" +
                $"Memory: {Cosmos.Core.GCImplementation.GetUsedRAM() / 1000000}/{Cosmos.Core.CPU.GetAmountOfRAM()} MB\n"*/;
        }
    }
}