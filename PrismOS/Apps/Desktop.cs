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
            Y = (int)(Kernel.Canvas.Height - 32);
            Width = Kernel.Canvas.Width;
            Height = 32;
            Text = "Desktop";
            HasBorder = false;
            Draggable = false;

            Windows.Add(this);
        }

        public void Add(Action OnClick)
        {
            Button Button1 = new();

            Button1.X = 32 * Elements.Count;
            Button1.Y = 0;
            Button1.Width = 32;
            Button1.Height = 32;
            Button1.HasBorder = true;
            Button1.OnClickEvents.Add(new((int X, int Y, Cosmos.System.MouseState State) => OnClick()));

            Elements.Add(Button1);
        }
    }
}