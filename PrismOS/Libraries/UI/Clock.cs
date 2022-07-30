using Cosmos.System;
using System;

namespace PrismOS.Libraries.UI
{
    public class Clock : Control
    {
        public Clock()
        {
            _ = new Cosmos.HAL.PIT.PITTimer(() => { ColonVisible = !ColonVisible; }, 1000000000, true);
        }

        public bool ColonVisible { get; set; }

        public override void Update(Window Parent)
        {
            if (Visible)
            {
                Width = Parent.Theme.Font.Width * 8;
                Height = Parent.Theme.Font.Height;

                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, Parent.Theme.Background);
                FrameBuffer.DrawString(Width / 2, Height / 2, DateTime.Now.ToString($"h{(ColonVisible ? ":" : " ")}mm tt"), Parent.Theme.Font, Parent.Theme.Foreground);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer, false);
            }
        }

        public override void OnKey(KeyEvent Key)
        {
        }
    }
}