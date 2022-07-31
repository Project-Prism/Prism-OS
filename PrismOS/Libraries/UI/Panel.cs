using Cosmos.System;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Panel : Control
    {
        public override void Update(Window Parent)
        {
            if (IsVisible)
            {
                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, Parent.Theme.Background);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer, false);
            }
        }

        public override void OnKey(KeyEvent Key)
        {
        }
    }
}