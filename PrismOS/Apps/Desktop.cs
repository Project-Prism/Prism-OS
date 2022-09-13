using PrismGL2D.UI;
using System;

namespace PrismOS.Apps
{
    public unsafe class Desktop : Frame
    {
        public Desktop()
        {
            // Main window
            X = 0;
            Y = (int)(Kernel.Canvas.Height - Config.Scale);
            Width = Kernel.Canvas.Width;
            Height = Config.Scale;
            Text = "Desktop";
            HasBorder = false;
            Draggable = false;

            Frames.Add(this);
        }

        public void Add(Action OnClick)
        {
            Button Button1 = new();

            Button1.X = (int)Config.Scale * Controls.Count;
            Button1.Y = 0;
            Button1.Width = Config.Scale;
            Button1.Height = Config.Scale;
            Button1.HasBorder = true;
            Button1.OnClickEvents.Add(new((int X, int Y, Cosmos.System.MouseState State) => OnClick()));

            Controls.Add(Button1);
        }
    }
}