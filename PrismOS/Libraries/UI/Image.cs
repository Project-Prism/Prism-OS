using PrismOS.Libraries.Graphics;
using Cosmos.System;

namespace PrismOS.Libraries.UI
{
    public class Image : Control
    {
        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            if (this.Buffer != null)
            {
                Buffer.DrawImage(X, Y, this.Buffer, false);
            }
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }
    }
}