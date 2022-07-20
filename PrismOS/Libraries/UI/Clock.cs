using PrismOS.Libraries.Graphics;
using System;

namespace PrismOS.Libraries.UI
{
    public class Clock : Control
    {
        public DateTime Time;

        public override void Update(Window Parent)
        {
            if (Visible)
            {
                int HW = Width / 2;
                int HH = Height / 2;

                FrameBuffer.DrawFilledCircle(HW, HH, Parent.Theme.Radius, Parent.Theme.Background);
                FrameBuffer.DrawAngledLine(HW, HH, DateTime.Now.Hour * 30, Parent.Theme.Radius - 45, Parent.Theme.Background);
                FrameBuffer.DrawAngledLine(HW, HH, DateTime.Now.Minute * 6, Parent.Theme.Radius - 25, Parent.Theme.Background);
                FrameBuffer.DrawAngledLine(HW, HH, DateTime.Now.Second * 6, Parent.Theme.Radius - 5, Parent.Theme.Accent);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }
        }
    }
}